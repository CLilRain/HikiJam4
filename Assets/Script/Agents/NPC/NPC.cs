using UnityEngine;

public class NPC : Interactable
{
    public static int PlayHashID = Animator.StringToHash("Play");
    public static int StopHashID = Animator.StringToHash("Stop");

    [Space]
    [SerializeField]
    private Animator charAnimator;

    [SerializeField]
    private RewardsAnimationController rewardAnimation;

    [Header("Dialogue")]
    [SerializeField]
    private TriggerCondition questItemReq;

    [SerializeField]
    [TextArea(0, 10)]
    private string defaultDialogue;

    private string[] defaultDialogueLines;

    [SerializeField]
    [TextArea(0, 10)]
    private string questCompletedDialogue;

    private string[] questCompletedDialogueLines;


    private GameManager gameManager;
    private UIManager ui;
    private Agent player;

    private string[] lines;
    private int lineIndex;
    private bool rewardGiven;

    public bool QuestCompleted { get; private set; } = false;

    private bool inConversation
    {
        get
        {
            if (gameManager != null)
            {
                return gameManager.GameState == GameState.Conversation;
            }
            return false;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        defaultDialogueLines = defaultDialogue.Split('\n');
        questCompletedDialogueLines = questCompletedDialogue.Split('\n');
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        ui = UIManager.Instance;
    }

    private void StartConversation()
    {
        gameManager.GoToConversationState(this);
        charAnimator.SetTrigger(PlayHashID);

        if (questItemReq != null && questItemReq.MeetsRequirement())
        {
            QuestCompleted = true;
        }

        if (QuestCompleted)
        {
            lines = questCompletedDialogueLines;
            Debug.Log($"<color=blue> Meets quest req for {objectName}</color>");
        }
        else
        {
            lines = defaultDialogueLines;
            Debug.Log($"<color=blue> Does not meet quest req for {objectName}</color>");
        }

        lineIndex = -1;

        ProgressConversation();
    }

    public void ProgressConversation()
    {
        SfxManager.Instance.PlayUIClick();

        if (++lineIndex < lines.Length)
        {
            ui.ShowDialogue(objectName, lines[lineIndex]);
        }
        else
        {
            StopConversation();
        }
    }

    private void StopConversation()
    {
        player = null;
        charAnimator.SetTrigger(StopHashID);
        ui.HideDialogue();

        if (QuestCompleted && !rewardGiven)
        {
            if (rewardAnimation != null)
            {
                rewardAnimation.Play();
            }

            SfxManager.Instance.PlayWinningSound();
            rewardGiven = true;
        }

        //Debug.Log($"Stop conversation. " +
        //    $"QuestCompleted {QuestCompleted} , rewardGiven {rewardGiven}");
        gameManager.ExitConversation();
    }

    protected override void Interact(Agent player)
    {
        this.player = player;

        if (defaultDialogue != string.Empty)
        {
            if (!inConversation)
            {
                StartConversation();
            }
        }
        else
        {
            charAnimator.SetTrigger(PlayHashID);
        }
    }
}
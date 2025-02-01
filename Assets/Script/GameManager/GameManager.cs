using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-10000000)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private QuestChecker questChecker;

    private NPC NPCInConversation;
    private GameStateController gameStateController;

    public GameState GameState => gameStateController.GameState;

    public static bool IsInGameplayMode
    {
        get
        {
            if (GameManager.Instance != null)
            {
                return GameManager.Instance.GameState == GameState.Playing;
            }
            return false;
        }
    }

    public static bool InConversation
    {
        get
        {
            if (Instance != null)
            {
                return Instance.GameState == GameState.Conversation;
            }

            return false;
        }
    }

    private readonly Color orange = new Color(255f, 165f, 0f);

    private void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("<color=red> ERROR: Duplicate GameManage found</color>", gameObject);
            //Destroy(gameObject);
            return;
        }

        questChecker = new QuestChecker();
        gameStateController = new GameStateController(this);

        GoToPlayingState();
    }

    private void Update()
    {
        switch (GameState)
        {
            case GameState.Playing:
                    break;
            case GameState.Conversation:
                if (Input.GetKeyDown(KeyCode.E) ||
                    Input.GetKeyDown(KeyCode.Return) ||
                    Input.GetKeyDown(KeyCode.Space))
                {
                    if (NPCInConversation != null)
                    {
                        NPCInConversation.ProgressConversation();
                    }
                }
                break;
            case GameState.GameOver:
            default:
                break;
        }
    }

    public void GoToPlayingState()
    {
        gameStateController.GoToGameState(GameState.Playing);
    }

    public void GoToConversationState(NPC NPCInConversation)
    {
        this.NPCInConversation = NPCInConversation;
        gameStateController.GoToGameState(GameState.Conversation, immediately: true);
    }

    public void ExitConversation()
    {
        if (questChecker.AreAllQuestsCompleted)
        {
            //GoToGameOverState();

            //Show game won screen
            //SceneManager.LoadScene(2);
        }
        else
        {
            GoToPlayingState();
        }
    }

    public void GoToGameOverState()
    {
        gameStateController.GoToGameState(GameState.GameOver, immediately: true);
    }
}

using UnityEngine;

public enum PlayerMode
{
    Walk,
    Plane,
}

public class PlayerController : Agent
{
    public static PlayerController instance;

    [Space]
    [SerializeField]
    private InteractableDetector detector;

    [SerializeField]
    private Transform repsawnPoint;

    [SerializeField]
    private float bottomBound = -70f;

    float cooldownTimer;
    AbilityManager abiilties;
    PlayerMode state;

    public int HP { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        abiilties = AbilityManager.Instance;

        GoToRespawnPosition();
    }

    private void Update()
    {
        if (!GameManager.IsInGameplayMode 
            || state == PlayerMode.Plane)
        {
            return;
        }

        if (detector.HasDetectedObject)
        {
            UIManager.Instance.SetDetectedInfo(detector.DetectedObject.ObjectName);

            if (Input.GetKeyDown(KeyCode.E) && detector.DetectedObject.TryInitiateInteraction(this))
            {
                UIManager.Instance.ClearDetectedInfo();
            }
        }

        if (transform.position.y < bottomBound)
        {
            GoToRespawnPosition();
        }
    }

    private void GoToRespawnPosition()
    {
        if (repsawnPoint != null)
        {
            transform.position = repsawnPoint.position;
        }
    }

    #region Item interaction

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.IsOnInteractableLayer())
        {
            var interactable = collider.GetComponent<Interactable>();
            if (interactable != null && interactable.IsInteractable)
            {
                interactable.OnCollision(this);
            }
        }
    }

    public void InteractsWithCampfire()
    {
        if (playerHand.ActiveWeapon != null)
        {
            var torch = (Torch)playerHand.ActiveWeapon;
            if (torch != null)
            {
                torch.FullyRecharge();
                Debug.Log("Torch fully recharged");
            }
            else
            {
                Debug.Log("Player does not have torch");
            }
        }
        else
        {
            Debug.Log("Player has not active weapon.");
        }
    }


    #endregion
}
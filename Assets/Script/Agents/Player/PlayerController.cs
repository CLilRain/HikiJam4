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

    float cooldownTimer;
    AbilityManager abiilties;
    PlayerMode state;

    const float bottomBound = -70f;
    public int HP { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        abiilties = AbilityManager.Instance;
    }

    void Update()
    {
        if (state == PlayerMode.Plane)
        {
            return;
        }

        if (detector.HasDetectedObject)
        {
            UIManager.Instance.SetDetectedInfo(detector.DetectedObject.ObjectName);

            if (Input.GetKeyDown(KeyCode.E) && detector.DetectedObject.TryInteract(this))
            {
                UIManager.Instance.ClearDetectedInfo();
            }
        }

        if (transform.position.y < bottomBound)
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
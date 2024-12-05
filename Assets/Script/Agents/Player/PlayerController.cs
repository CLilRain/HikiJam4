using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode
{
    Walk,
    Plane,
}

public class PlayerController : Agent
{
    public static PlayerController instance;

    [Header("Detection")]
    [SerializeField]
    private PlayerEyesDetector eyeDetector;

    [Header("Movement")]
    [SerializeField]
    private FirstPersonMovement playerMove;
    [SerializeField]
    private FirstPersonLook playerLook;


    [SerializeField]
    private Transform repsawnPoint;

    float cooldownTimer;
    int essence;
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

        DetectItem();

        //if (Input.GetMouseButtonDown(0))
        //{
        //    abiilties.ShowGhostCloud();
        //    // preview ghost
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    abiilties.SpawnCloud();
        //}

        if (transform.position.y < bottomBound)
        {
            transform.position = repsawnPoint.position;
        }
    }

    private void DetectItem()
    {
        eyeDetector.DetectItem();
        if (eyeDetector.hasFoundObject)
        {
            eyeDetector.foundObject.OnDetection();

            if (Input.GetKeyDown(KeyCode.E))
            {
                eyeDetector.foundObject.Interact(this);
            }
        }
    }


    #region Airplane

    public void EnterPlane(Plane plane)
    {
        playerMove.Disable();
        state = PlayerMode.Plane;
        //transform.position = plane.cockpit.position;
        //transform.parent = plane.cockpit.parent;

        //plane.PlayerEnters(this);
    }

    public void ExitPlane()
    {
        playerMove.Enable();
        state = PlayerMode.Walk;

        transform.parent = null;
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

    }

    #endregion


    #region Collision
    private void OnTriggerEnter(Collider other)
    {
        if (IsInteractable(other))
        {
            var interactable = other.GetComponent<DetectionReceiver>();

            if (interactable != null)
            {
                switch (interactable.GetCollectableType())
                {
                    case InteractableTypes.Pickup:
                        Debug.Log("Player trigger entered Pickup " + other.name, gameObject);
                        interactable.Interact(this);
                        break;
                }
            }
        }
    }

    public void CollectEssence()
    {
        essence++;
        UpdateCoinScore();
    }

    private void UpdateCoinScore()
    {
        UIManager.Instance.SetEssence(essence);
    }

    private bool IsInteractable(Collider other)
    {
        return GameSettings.Instance.interactableLayer.Contains(other.gameObject);
        //return (1 << other.gameObject.layer) == GameSettings.Instance.collectableLayer;
    }

    #endregion
}
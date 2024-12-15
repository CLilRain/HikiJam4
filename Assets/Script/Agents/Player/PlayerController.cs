using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

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
    private Transform repsawnPoint;

    float cooldownTimer;
    int essence;
    AbilityManager abiilties;
    PlayerMode state;

    Interactable collidingInteractable;

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

        if (collidingInteractable != null 
            && collidingInteractable.IsInteractable 
            && Input.GetKeyDown(KeyCode.E))
        {
            collidingInteractable.Interact(this);
        }

        if (transform.position.y < bottomBound)
        {
            transform.position = repsawnPoint.position;
        }
    }

    #region Item interaction

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

    private void OnTriggerEnter(Collider other)
    {
        if (IsInteractable(other))
        {
            var interactable = other.GetComponent<Interactable>();
            collidingInteractable = interactable;

            if (interactable != null)
            {
                interactable.OnCollision(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsInteractable(other))
        {
            var interactable = other.GetComponent<Interactable>();
            ExitsCollidingWithInteractable(interactable);
        }
    }

    private void ExitsCollidingWithInteractable(Interactable interactable)
    {
        if (collidingInteractable != null && collidingInteractable == interactable)
        {
            interactable = null;
        }
    }

    #endregion

    #region Interaction with specific objects

    public override void CollectEssence()
    {
        essence++;
        UpdateCoinScore();
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
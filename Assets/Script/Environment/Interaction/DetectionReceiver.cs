using System;
using UnityEngine;

public enum InteractableTypes
{
    Pickup,

    // Weapons
    Weapon,

}

public class DetectionReceiver : MonoBehaviour
{
    public event Action<PlayerController> OnInteraction;

    [SerializeField]
    private string objectName = "UnAssignedName";

    [SerializeField]
    private Collider interactionDetectorCollider;

    public bool Interactable { get; private set; } = true;

    protected bool HasSubscriber => OnInteraction != null;

    public InteractableTypes GetCollectableType() => InteractableTypes.Pickup;

    private void Awake()
    {
        if (interactionDetectorCollider == null)
        {
            interactionDetectorCollider = GetComponent<Collider>();
        }
    }

    public virtual void Interact(PlayerController player)
    {
        if (OnInteraction != null)
        {
            Debug.Log($"Player interacted with {objectName}");
            OnInteraction.Invoke(player);
            //return true;
        }
        //else
        //{
        //    return false;
        //}
    }

    public virtual void EnableInteraction()
    {
        Interactable = true;
        interactionDetectorCollider.enabled = true;
    }

    public virtual void DisableInteraction()
    {
        Interactable = false;
        interactionDetectorCollider.enabled = false;
    }

    public virtual void OnDetection()
    {
        UIManager.Instance.SetDetectedInfo(objectName);
    }


}
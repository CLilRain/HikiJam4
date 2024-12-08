using System;
using UnityEngine;

public enum InteractableTypes
{
    Pickup,
    Weapon,
    Environment,
}

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    protected string objectName = "UnAssignedName";

    protected Collider interactionDetectorCollider;

    // Event is used when an interactable object cannot neatly inherit from this class.
    //protected bool HasSubscriber => OnInteraction != null;
    //public event Action<Agent> OnInteraction;

    public bool IsInteractable { get; private set; } = true;
    public InteractableTypes GetInteractableType() => InteractableTypes.Pickup;

    protected virtual void Awake()
    {
        interactionDetectorCollider = GetComponent<Collider>();
    }

    public virtual void Interact(Agent player)
    {
        Debug.Log($"{player.name} interacted with {objectName}");
      
    }

    public virtual void OnCollision(Agent player)
    {
        Debug.Log($"{player.name} collide with {objectName}");
    }

    public virtual void EnablePickupInteraction()
    {
        IsInteractable = true;
        interactionDetectorCollider.enabled = true;
    }

    public virtual void DisablePickupInteraction()
    {
        IsInteractable = false;
        interactionDetectorCollider.enabled = false;
    }

    public virtual void OnDetection()
    {
        UIManager.Instance.SetDetectedInfo(objectName);
    }
}
using System;
using UnityEngine;



[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    protected string objectName = "UnAssignedName";

    [SerializeField]
    protected InteractableTypes interactableType;

    protected Collider interactionDetectorCollider;

    // Event is used when an interactable object cannot neatly inherit from this class.
    //protected bool HasSubscriber => OnInteraction != null;
    //public event Action<Agent> OnInteraction;

    public string ObjectName => objectName;
    public InteractableTypes GetInteractableType() => interactableType;

    public bool IsInteractable { get; protected set; } = true;

    protected virtual void Awake()
    {
        interactionDetectorCollider = GetComponent<Collider>();
    }

    public virtual bool TryInteract(Agent player)
    {
        if (IsInteractable)
        {
            Debug.Log($"<color=green>{player.name} interacted with {objectName}</color>");
            DisableInteraction();
            return true;
        }

        Debug.Log($"<color=red>{player.name} cannot interact with disabled {objectName}</color>");

        return false;
    }

    public virtual void OnCollision(Agent player)
    {
        Debug.Log($"{player.name} collide with {objectName}");
    }

    public virtual void EnableInteraction()
    {
        IsInteractable = true;
        interactionDetectorCollider.enabled = true;
    }

    public virtual void DisableInteraction()
    {
        IsInteractable = false;
        interactionDetectorCollider.enabled = false;
    }
}
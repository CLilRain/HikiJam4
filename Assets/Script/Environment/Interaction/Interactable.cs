using System;
using UnityEngine;

public enum InteractableTypes
{
    Pickup,

    // Weapons
    Weapon,

}

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [SerializeField]
    private string objectName = "UnAssignedName";

    private Collider interactionDetectorCollider;

    public event Action<Agent> OnInteraction;
    public bool IsInteractable { get; private set; } = true;
    protected bool HasSubscriber => OnInteraction != null;
    public InteractableTypes GetCollectableType() => InteractableTypes.Pickup;

    private void Awake()
    {
        interactionDetectorCollider = GetComponent<Collider>();
    }

    public virtual void Interact(Agent player)
    {
        if (OnInteraction != null)
        {
            Debug.Log($"{player.name} interacted with {objectName}");
            OnInteraction.Invoke(player);
            //return true;
        }
        //else
        //{
        //    return false;
        //}
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

    public virtual void OnDetection()
    {
        UIManager.Instance.SetDetectedInfo(objectName);
    }
}
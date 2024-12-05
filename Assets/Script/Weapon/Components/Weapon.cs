using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    int attackDamage;

    [SerializeField]
    private DetectionReceiver playerInteractionReceiver;

    [SerializeField]
    InteractableTypes interactableType = InteractableTypes.Weapon;

    Agent owner;

    public int AttackDamage => attackDamage;

    private void Awake()
    {
        playerInteractionReceiver.OnInteraction -= OnPickUp;
        playerInteractionReceiver.OnInteraction += OnPickUp;
    }

    private void OnEnable()
    {
        playerInteractionReceiver.OnInteraction -= OnPickUp;
        playerInteractionReceiver.OnInteraction += OnPickUp;
    }

    // Pickup
    public virtual void OnPickUp(Agent owner)
    {
        this.owner = owner;

        owner.PickUpWeapon(this);
        playerInteractionReceiver.DisableInteraction();
        //EnableEffect();
    }

    public virtual void OnDrop()
    {
        owner = null;

        // TODO: leaves player body
    }

    // Action
    public virtual void Strike()
    {
    }

    public virtual void Sheeth()
    {
    }

    // Effect
    public virtual void EnableEffect()
    {
    }

    public virtual void DisableEffect()
    {
    }

}
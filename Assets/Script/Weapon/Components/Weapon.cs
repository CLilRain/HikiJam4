using UnityEngine;

public abstract class Weapon : Interactable
{
    [SerializeField]
    protected int attackDamage;

    [SerializeField]
    protected Collider damageCollider;

    protected Agent owner;

    public int AttackDamage => attackDamage;

    // Pickup
    public virtual void OnPickUp(Agent owner)
    {
        this.owner = owner;

        // TODO: Change gameobject layer to weapons layer

        DisablePickupInteraction();
        //EnableEffect();
    }

    public virtual void OnDrop()
    {
        owner = null;
        EnablePickupInteraction();

        // TODO: leaves player body
    }

    // Action
    public virtual void Strike()
    {
    }

    public virtual void Sheeth()
    {
    }

    public virtual void Enable(Agent owner)
    {
        this.owner = owner;
    }

    public virtual void Disable()
    {
        owner = null;
    }
}
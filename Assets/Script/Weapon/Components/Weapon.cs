using UnityEngine;

public abstract class Weapon : Interactable
{
    [SerializeField]
    protected int attackDamage;

    [SerializeField]
    protected WeaponCollider damageCollider;

    protected Agent owner;

    public int AttackDamage => attackDamage;

    protected override void Awake()
    {
        base.Awake();
        damageCollider.Init(this);
    }

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
        damageCollider.Enable();
    }

    public virtual void Sheeth()
    {
        damageCollider.Disable();
    }

    public virtual void PickedUpByAgent(Agent owner)
    {
        this.owner = owner;
    }

    public virtual void DroppedByAgent()
    {
        owner = null;
    }

    public virtual void HitsCollider(Collider hitCollider)
    {
        if (GameSettings.Instance.interactableLayer.Contains(hitCollider.gameObject))
        {
            Debug.Log($"{owner.name}'s {this.name} hits {hitCollider.name}");
        }
    }
}
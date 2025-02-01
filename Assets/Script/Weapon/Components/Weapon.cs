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

    public override bool TryInitiateInteraction(Agent player)
    {
        if (IsInteractable)
        {
            player.PickUpWeapon(this);
            owner = player;
        }

        return base.TryInitiateInteraction(player);
    }

    public virtual void OnDrop()
    {
        owner = null;
        EnableInteraction();

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

    public virtual void HitsCollider(Collider hitCollider)
    {
        if (GameSettings.Instance.interactableLayer.Contains(hitCollider.gameObject))
        {
            Debug.Log($"{owner.name}'s {this.name} hits {hitCollider.name}");
        }
    }
}
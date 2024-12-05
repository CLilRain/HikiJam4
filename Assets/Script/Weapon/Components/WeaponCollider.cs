using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    private Agent owner;

    private Weapon weapon;

    public void Enable(Agent owner, Weapon weapon)
    {
        this.owner = owner;
        this.weapon = weapon;
    }

    public void Disable()
    {
        owner = null;
        weapon = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameSettings.Instance.interactableLayer.Contains(other.gameObject))
        {
            var interactor = other.GetComponent<DamageReceiver>();

            if (interactor != null && interactor.Owner != owner)
            {
                interactor.ReceiveDamage(owner, weapon.AttackDamage);
            }
        }
    }
}

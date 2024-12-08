using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    private Agent owner;

    private Weapon weapon;



    private void OnTriggerEnter(Collider other)
    {
        if (GameSettings.Instance.interactableLayer.Contains(other.gameObject))
        {
            Debug.Log($"{owner.name}'s weapon hits {other.name}");
            //var interactor = other.GetComponent<DamageReceiver>();

            //if (interactor != null && interactor.Owner != owner)
            //{
            //    interactor.ReceiveDamage(owner, weapon.AttackDamage);
            //}
        }
    }
}

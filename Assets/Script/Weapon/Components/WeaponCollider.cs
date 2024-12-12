using System;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField]
    private Collider damageCollider;

    private Weapon weapon;

    public event Action OnTrigger;

    public void Init(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public void Enable()
    {
        damageCollider.enabled = true;
    }

    public void Disable()
    {
        damageCollider.enabled = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        weapon.HitsCollider(other);
    }
}

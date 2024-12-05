using System.Collections;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField]
    protected PlayerHand playerHand;


    public virtual void ReceiveDamage(Agent attacker, int damage)
    {

    }

    public void PickUpWeapon(Weapon weapon)
    {
        // TODO: Inventory processing

        playerHand.AssignWeapon(weapon);
    }

}

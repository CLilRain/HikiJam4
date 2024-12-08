using System.Collections;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    [SerializeField]
    protected string agentName = "Unassgined name";

    [Header("Weapon")]
    [SerializeField]
    protected PlayerHand playerHand;

    public virtual void ReceiveDamage(Agent attacker, int damage) {}
    public virtual void CollectEssence() {}

    public void PickUpWeapon(Weapon weapon)
    {
        // TODO: Inventory processing

        playerHand.AssignWeapon(weapon);
        weapon.OnPickUp(this);
    }
}
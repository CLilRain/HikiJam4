using System.Collections;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    [SerializeField]
    protected string agentName = "Unassgined name";

    [Header("Weapon")]
    [SerializeField]
    protected PlayerHand playerHand;

    protected int essence;

    public virtual void ReceiveDamage(Agent attacker, int damage) {}

    public void PickUpWeapon(Weapon weapon)
    {
        // TODO: Inventory processing

        playerHand.AssignWeapon(weapon);
    }

    public void PickUpItem(ItemTypes itemType)
    {
        switch (itemType)
        {
            case ItemTypes.Essence:
                essence++;
                UIManager.Instance.SetEssence(essence);
                break;
            case ItemTypes.Router:
            case ItemTypes.WaterPot:
            case ItemTypes.RiceBag:
            default:
                UIManager.Instance.PickedUpItem(itemType);
                break;
        }

    }
}
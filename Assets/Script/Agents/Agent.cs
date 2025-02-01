using System.Collections;
using UnityEditor;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    [SerializeField]
    protected string agentName = "Unassgined name";

    [Header("Weapon")]
    [SerializeField]
    protected PlayerHand playerHand;

    public virtual void ReceiveDamage(Agent attacker, int damage) {}

    public void PickUpWeapon(Weapon weapon)
    {
        // TODO: Inventory processing

        playerHand.AssignWeapon(weapon);
    }

    public void PickUpItem(ItemTypes itemType)
    {
        Debug.Log($"<color=yellow> Picks up item {itemType}</color>");

        switch (itemType)
        {
            case ItemTypes.Essence:
                PlayerData.Essence++;
                UIManager.Instance.SetEssence(PlayerData.Essence);
                break;

            case ItemTypes.Router:
                PlayerData.HasRouter = true;
                UIManager.Instance.UpdateItemDisplay(itemType, true);
                break;

            case ItemTypes.WaterPot:
                PlayerData.HasWaterPot = true;
                UIManager.Instance.UpdateItemDisplay(itemType, true);
                break;

            case ItemTypes.RiceBag:
            default:
                UIManager.Instance.UpdateItemDisplay(itemType, true);
                PlayerData.HasRiceBag = true;
                break;
        }
    }
}
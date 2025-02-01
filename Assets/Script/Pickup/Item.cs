using System.Collections;
using UnityEngine;

public class Item : Interactable
{
    [SerializeField]
    public ItemTypes itemType;

    protected override void Interact(Agent player)
    {
        player.PickUpItem(itemType);

        switch (itemType)
        {
            case ItemTypes.Essence:
                SfxManager.Instance.PlayUIClick();
                break;
            default:
                SfxManager.Instance.PlayPickupSound();
                break;
        }

        Destroy(gameObject);
    }
}
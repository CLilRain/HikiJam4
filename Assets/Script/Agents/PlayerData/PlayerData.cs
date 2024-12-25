using System.Collections;
using UnityEngine;

public static class PlayerData
{
    public static int Essence = 0;
    public static bool HasRouter;
    public static bool HasWaterPot;
    public static bool HasRiceBag;

    public static bool HasItem(ItemTypes itemType)
    {
        switch (itemType)
        {
            case ItemTypes.Router:
                return HasRiceBag;
            case ItemTypes.WaterPot:
                return HasWaterPot;
            case ItemTypes.RiceBag:
                return HasRiceBag;
            default:
                Debug.LogError("Item type not found");
                return true;
        }
    }
}
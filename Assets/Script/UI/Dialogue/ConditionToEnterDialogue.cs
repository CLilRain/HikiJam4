using UnityEngine;

//[System.Serializable]
//public class StatsConditionsToEnter
//{
//    public Stats stat;
//    public int quantity;
//}

[System.Serializable]
public class TriggerCondition
{
    public ItemTypes[] ItemsNeeded;
    //public StatsConditionsToEnter[] StatConditionsToEnter;

    public bool MeetsRequirement()
    {
        foreach (var item in ItemsNeeded)
        {
            if (!PlayerData.HasItem(item))
            {
                return false;
            }
        }
        return true;
    }
}
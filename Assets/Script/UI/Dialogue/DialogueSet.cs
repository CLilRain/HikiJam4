using UnityEngine;

[CreateAssetMenu(fileName = "Create Dialogue", menuName = "Scriptable Objects/ Create Dialogue")]
public class DialogueSet : ScriptableObject
{
    public string Speaker;

    [Header("Condition To Enter")]
    public DialogueEntry[] entries;

    public DialogueEntry GetDialogueEntry()
    {
        foreach (var entry in entries)
        {
            // Check display conditions
            if (PlayerData.HasItem(entry.conditionItem))
            {
                return entry;
            }
        }

        Debug.LogError("No valid dialogue entry found");
        return entries[0];
    }
}

[System.Serializable]
public class DialogueEntry: ScriptableObject
{
    [Header("Condition To Display")]
    public ItemTypes conditionItem;

    [Header("Dialogue content")]
    public string[] lines;

    public DialogueSet nextSet;
}
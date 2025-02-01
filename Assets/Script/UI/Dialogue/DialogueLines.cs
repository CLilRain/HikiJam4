using UnityEngine;

[CreateAssetMenu(fileName = "Create DialogueLines", 
    menuName = "Scriptable Objects/ Create DialogueLines")]
public class DialogueLines: ScriptableObject
{
    [Header("Condition To Display")]
    public TriggerCondition[] enterRequiment;

    [Header("Dialogue content")]
    public string[] lines;

    public bool completed = false;

    public bool CanEnter()
    {
        if (completed)
        {
            return false;
        }

        foreach (var req in enterRequiment)
        {
            if (!req.MeetsRequirement())
            {
                return false;
            }
        }

        return true;
    }
}

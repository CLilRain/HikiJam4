using UnityEngine;

[CreateAssetMenu(fileName = "Create NPCDialogueContainer", 
    menuName = "Scriptable Objects/ Create NPCDialogueContainer")]
public class NPCDialogueContainer : MonoBehaviour
{
    [SerializeField]
    private TriggerCondition ConditionToEnter;

    [SerializeField]
    private DialogueLines defaultDialogue;

    [SerializeField]
    private DialogueLines[] conditionalDialogues;

    public DialogueLines GetNextDialogueLine()
    {
        foreach (var branch in conditionalDialogues)
        {
            if (branch.CanEnter())
            {
                return branch;
            }
        }

        return defaultDialogue;
    }
}


/*
 * dialogue manager
 * start dialogue (Action on complete callback)
 * dialogueOptions[]
 * 
 * 
 dialogue option:

- enter condition
- lines
- display options
- selection effect
 
 */
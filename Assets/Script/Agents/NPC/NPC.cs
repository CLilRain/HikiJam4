using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public static int PlayHashID = Animator.StringToHash("Play");
    public static int StopHashID = Animator.StringToHash("Stop");

    public Animator animator;
    public ItemTypes questItem;

    [SerializeField]
    private DialogueSet startingDialogue;

    Agent agent;
    bool inConversation;
    bool givenQuestItem;

    void Interact()
    {
        if (inConversation)
        {
            StopConversation();
        }
        else
        {
            StartConversation();
        }

    }

    private void StartConversation()
    {
        if (!UIManager.Instance.InDialogue)
        {
            inConversation = true;
            animator.SetTrigger(PlayHashID);
            

        }

        if (agent)
        {

        }
    }

    private void StopConversation()
    {
        inConversation = false;
        animator.SetTrigger(StopHashID);
        agent = null;
    }

    public override bool TryInteract(Agent agent)
    {
        if (IsInteractable)
        {
            this.agent = agent;
            Interact();
        }

        Debug.Log($"<color=green>{agent.name} interacted with {objectName}</color>");
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public Animator animator;

    public ItemTypes questItem;

    public static int PlayHashID = Animator.StringToHash("Play");
    public static int StopHashID = Animator.StringToHash("Stop");

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

        inConversation = !inConversation;
    }

    private void StartConversation()
    {
        animator.SetTrigger(PlayHashID);
    }

    private void StopConversation()
    {
        animator.SetTrigger(StopHashID);
    }

    public override bool TryInteract(Agent agent)
    {
        if (IsInteractable)
        {
            Interact();
        }

        Debug.Log($"<color=green>{agent.name} interacted with {objectName}</color>");
        return true;
    }
}

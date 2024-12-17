using System.Collections;
using UnityEngine;

public class Item : Interactable
{
    [SerializeField]
    public ItemTypes itemType;

    public override bool TryInteract(Agent agent)
    {
        if (IsInteractable)
        {
            agent.PickUpItem(itemType);
            Destroy(gameObject);
        }

        return base.TryInteract(agent);
    }
}
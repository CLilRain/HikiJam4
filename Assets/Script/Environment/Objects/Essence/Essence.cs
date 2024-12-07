using System.Collections;
using UnityEngine;

public class Essence : Interactable
{
    public override void Interact(Agent agent)
    {
        agent.CollectEssence();

        Destroy(gameObject);
    }
}
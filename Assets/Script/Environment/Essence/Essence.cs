using System.Collections;
using UnityEngine;

public class Essence : MonoBehaviour
{
    [SerializeField]
    private Interactable playerInteractionReceiver;

    void Awake()
    {
        playerInteractionReceiver.OnInteraction -= Interact;
        playerInteractionReceiver.OnInteraction += Interact;
    }

    private void OnDestroy()
    {
        playerInteractionReceiver.OnInteraction -= Interact;
    }

    private void Interact(Agent agent)
    {
        agent.CollectEssence();

        Destroy(gameObject);
    }
}
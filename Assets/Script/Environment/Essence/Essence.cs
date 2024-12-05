using System.Collections;
using UnityEngine;

public class Essence : MonoBehaviour
{
    [SerializeField]
    private DetectionReceiver playerInteractionReceiver;

    void Awake()
    {
        playerInteractionReceiver.OnInteraction -= Interact;
        playerInteractionReceiver.OnInteraction += Interact;
    }

    private void OnDestroy()
    {
        playerInteractionReceiver.OnInteraction -= Interact;
    }

    private void Interact(PlayerController player)
    {
        player.CollectEssence();

        Destroy(gameObject);
    }
}
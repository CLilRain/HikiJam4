using System.Collections;
using UnityEngine;

public class Campfire : Interactable
{
    [SerializeField]
    ParticleSystem particle;

    public bool IsLit { get; private set; }

    public void Lit()
    {
        particle.Play();
    }

    protected override void Interact(Agent player)
    {
        PlayerController mainPlayer = (PlayerController)player;

        if (mainPlayer != null)
        {
            mainPlayer.InteractsWithCampfire();
            SfxManager.Instance.PlayTorchSound();
        }
    }
}
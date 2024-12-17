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

    public override bool TryInteract(Agent player)
    {
        if (IsInteractable)
        {
            PlayerController mainPlayer = (PlayerController)player;

            if (mainPlayer != null)
            {
                mainPlayer.InteractsWithCampfire();
            }
        }

        return base.TryInteract(player);
    }
}
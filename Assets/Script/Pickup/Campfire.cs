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

    public override void Interact(Agent player)
    {
        base.Interact(player);
        PlayerController mainPlayer = (PlayerController)player;

        if (mainPlayer != null)
        {
            mainPlayer.InteractsWithCampfire();
        }
    }

    public override void OnCollision(Agent player)
    {
        base.OnCollision(player);
        Interact(player);
    }
}
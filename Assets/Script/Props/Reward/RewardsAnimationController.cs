using System.Collections;
using UnityEngine;

public class RewardsAnimationController : MonoBehaviour
{
    [SerializeField]
    private ScaleUpExpander[] expandingObjects;

    [SerializeField]
    private ParticleSystem[] particles;

    public void Play()
    {
        foreach (var obj in expandingObjects)
        {
            obj.Play();
        }

        foreach (var obj in particles)
        {
            obj.Play();
        }
    }
}
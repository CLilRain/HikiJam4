using System.Collections;
using UnityEngine;

public class AnimationPlayHandler : MonoBehaviour
{
    Animation animator;

    private void Awake()
    {
        animator = GetComponent<Animation>();
    }
    public void Play()
    {
        if (animator != null)
        {
            animator.Play("Play");
        }
    }
}

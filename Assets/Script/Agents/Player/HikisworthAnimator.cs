using System.Collections;
using UnityEngine;

public class HikisworthAnimator : MonoBehaviour
{
    Animator animator;

    readonly int OnGrond = Animator.StringToHash("OnGround");
    readonly int Victory = Animator.StringToHash("Victory");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
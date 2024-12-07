using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Animator meshAnimator;


    [SerializeField]
    private Jump jump;
    [SerializeField]
    private GroundCheck GroundCheck;
    [SerializeField]
    private Crouch crouch;

    public float speed = 5;

    [Header("Running")]
    [SerializeField]
    private bool canRun = true;

    [SerializeField]
    private float runSpeed = 9;

    [SerializeField]
    private KeyCode runningKey = KeyCode.LeftShift;

    private Rigidbody rb;

    private MonoBehaviour[] movementComponents;

    public bool IsRunning { get; private set; }

    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        // Get the rigidbody on this.
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        IsRunning = canRun && Input.GetKey(runningKey);

        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector2 targetVelocity = new Vector2(x * targetMovingSpeed, z * targetMovingSpeed);

        rb.velocity = transform.rotation * new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.y);
        meshAnimator.SetFloat("X", x);
        meshAnimator.SetFloat("Z", z);
    }

    public void Enable()
    {
        SetComponentEnableDisable(true);
    }
    public void Disable()
    {
        SetComponentEnableDisable(false);
    }

    void SetComponentEnableDisable(bool isEnabled)
    {
        foreach (var component in movementComponents)
        {
            component.enabled = isEnabled;
        }
        this.enabled = isEnabled;
    }
}
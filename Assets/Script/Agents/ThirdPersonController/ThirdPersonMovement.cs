using UnityEngine;

public class ThirdPersonMovement : PlayerMovement
{
    [Header("References")]
    [SerializeField]
    private Animator characterAnimator;

    [SerializeField]
    private Transform characterMesh;

    [SerializeField]
    private Transform cameraTransform;

    [Header("Settings")]
    [SerializeField]
    private float moveSpeed = 5;

    [SerializeField]
    private float turnSpeed = 15;

    [SerializeField]
    private bool withStrafing;

    private Rigidbody rb;

    private float inputX;
    private float inputZ;

    void Awake()
    {
        // Get the rigidbody on this.
        rb = GetComponent<Rigidbody>();

        if (cameraTransform == null)
        {
            Debug.Log("Camera transform is not attached", gameObject);
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        // Input
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        Quaternion cameraRotation = cameraTransform.UntiltedRotationFacingFront();

        // If moving, then rotate the character
        if (Mathf.Abs(inputX) > Mathf.Epsilon || Mathf.Abs(inputZ) > Mathf.Epsilon)
        {
            Quaternion targetRot = cameraRotation;

            if (!withStrafing)
            {
                Vector3 facingDir = new Vector3(inputX, 0f, inputZ);
                targetRot = cameraRotation * Quaternion.LookRotation(facingDir);
            }

            characterMesh.rotation = Quaternion.Lerp(characterMesh.rotation, targetRot, turnSpeed * Time.deltaTime);
        }

        var moveX = inputX * moveSpeed;
        var moveZ = inputZ * moveSpeed;
        rb.velocity = cameraRotation * new Vector3(moveX, rb.velocity.y, moveZ);

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        if (characterAnimator == null)
        {
            return;
        }

        if (withStrafing)
        {
            characterAnimator.SetFloat("X", inputX);
            characterAnimator.SetFloat("Z", inputZ);
        }
        else
        {
            var move = Mathf.Abs(inputX) + Mathf.Abs(inputZ);
            if (move > 1f)
            {
                move = 1f;
            }
            characterAnimator.SetFloat("Z", move);
        }
    }
}
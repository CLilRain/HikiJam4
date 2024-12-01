using UnityEngine;
using static UnityEngine.UI.Image;

[ExecuteInEditMode]
public class GroundCheck : MonoBehaviour
{
    [Tooltip("Maximum distance from the ground.")]
    public float checkDistance = .5f;

    [Tooltip("Whether this transform is grounded now.")]
    public bool isGrounded = true;

    public float xOffset = 0.6f;
    /// <summary>
    /// Called when the ground is touched again.
    /// </summary>
    public event System.Action Grounded;

    const float OriginOffset = .3f;
    Vector3 RaycastOrigin => transform.position + Vector3.up * OriginOffset;

    Vector3[] groundOrigins;
    Vector3 sideOrigin;
    Vector3[] sideDirections;

    private void Awake()
    {
        groundOrigins = new Vector3[5];
        groundOrigins[0] = Vector3.zero;
        groundOrigins[1] = new Vector3(-xOffset, OriginOffset, xOffset);
        groundOrigins[2] = new Vector3(xOffset, OriginOffset, -xOffset);
        groundOrigins[3] = new Vector3(xOffset, OriginOffset, xOffset);
        groundOrigins[4] = new Vector3(-xOffset, OriginOffset, -xOffset);

        sideOrigin = new Vector3(0f, OriginOffset, 0f);

        sideDirections = new Vector3[4]
        {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right,
        };
    }

    void LateUpdate()
    {
        // Check if we are grounded now.
        bool isGroundedNow = false;
        isGroundedNow = CheckIsOnGround();

        // Call event if we were in the air and we are now touching the ground.
        if (isGroundedNow && !isGrounded)
        {
            Grounded?.Invoke();
        }

        // Update isGrounded.
        isGrounded = isGroundedNow;
    }

    bool CheckIsOnGround()
    {
        foreach (var origin in groundOrigins)
        {
            Debug.DrawRay(transform.position + origin, Vector3.down * checkDistance);
            if (Physics.Raycast(transform.position + origin, Vector3.down, checkDistance))
            {
                return true;
            }
        }

        foreach (var dir in sideDirections)
        {
            Debug.DrawRay(transform.position + sideOrigin, dir * checkDistance);
            if (Physics.Raycast(transform.position + sideOrigin, dir, checkDistance))
            {
                return true;
            }
        }

        return false;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a line in the Editor to show whether we are touching the ground.
        Debug.DrawLine(RaycastOrigin, RaycastOrigin + Vector3.down * checkDistance, isGrounded ? Color.white : Color.red);
    }
}

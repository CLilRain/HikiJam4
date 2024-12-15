using UnityEngine;

public static class UntiltedRotation
{
    public static Quaternion UntiltedRotationFacingFront(this Transform transform)
    {
        var front = transform.forward;
        front.y = 0f;
        front.Normalize();

        return Quaternion.LookRotation(front, Vector3.up);
    }
}

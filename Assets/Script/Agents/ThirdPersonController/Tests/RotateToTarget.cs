using System.Collections;
using UnityEngine;
using UnityEditor;

public class RotateToTarget : MonoBehaviour
{
    public Transform target;

    public float tiltAngle = 25f;
    public float distance = 5;


    public void LookTowardsTargetAtFixedDistance()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        transform.rotation = rotation;

        transform.position = target.position + rotation * Vector3.back * distance;
        //targetRotation = Quaternion.LookRotation(transform.position - target.position, Vector3.up);
    }

    public void LookTowardsTargetWithTilt()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        Quaternion baseRotation = Quaternion.LookRotation(direction, Vector3.up);
        Quaternion tiltRotation = Quaternion.Euler(tiltAngle, 0f, 0f);

        transform.rotation = baseRotation * tiltRotation;
        transform.position = target.position + transform.rotation * new Vector3(0f, 0f, -distance);
    }

    public void MoveToBehindTargetWithTilt()
    {
        var rotationWithTile = target.rotation * Quaternion.Euler(tiltAngle, 0f, 0f);
        transform.rotation = rotationWithTile;
        transform.position = target.position + rotationWithTile * new Vector3(0f, 0f, -distance);
        //targetRotation = Quaternion.LookRotation(transform.position - target.position, Vector3.up);
    }
}
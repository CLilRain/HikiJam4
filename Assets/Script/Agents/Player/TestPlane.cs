using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlane : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 50f; // Constant forward speed

    [Header("Rotation Settings")]
    public float pitchSpeed = 50f; // Nose up/down
    public float yawSpeed = 50f;   // Turn left/right
    public float rollSpeed = 50f;  // Tilt for better turning

    void Update()
    {
        // Constant forward movement
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Get input for pitch and yaw
        float pitch = Input.GetAxis("Vertical") * pitchSpeed * Time.deltaTime; // W/S: Nose up/down
        float yaw = Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;  // A/D: Turn left/right
        float roll = -Input.GetAxis("Horizontal") * rollSpeed * Time.deltaTime; // Roll based on turning

        // Apply rotation
        transform.Rotate(pitch, yaw, roll);
    }
}

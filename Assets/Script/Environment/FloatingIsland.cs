using UnityEngine;
using static UnityEditor.PlayerSettings;

public class FloatingIsland : MonoBehaviour
{
    [Header("Rotating")]
    public float rotatingSpeed = 5f;

    [Header("Bobbing")]
    public float bobTimerSpeed = 0.2f;
    public float bobMagnitude = 2f;

    float startPosY;
    float timer;

    float invertRadius;
    float orbitRadius;
    float currentAngle;

    Transform cachedTransform;
    Vector3 pos;

    void Start()
    {
        cachedTransform = transform;

        startPosY = transform.position.y;
        timer = Random.Range(0f, 1f);

        // Calculate the initial angle based on the object's starting position
        Vector3 pos = transform.position;
        currentAngle = Mathf.Atan2(pos.z, pos.x) * Mathf.Rad2Deg;

        // Ensure orbit radius matches the starting position
        orbitRadius = pos.magnitude;
        invertRadius = 1 / orbitRadius;
    }

    void FixedUpdate()
    {
        // Bobbing
        timer += Time.fixedDeltaTime * bobTimerSpeed;
        if (timer > Mathf.Rad2Deg)
        {
            timer -= Mathf.Rad2Deg;
        }
        pos.y = Mathf.Sin(timer) * bobMagnitude;

        // Rotation
        currentAngle += rotatingSpeed * invertRadius * Time.fixedDeltaTime;
        currentAngle %= 360;

        float radian = Mathf.Deg2Rad * currentAngle;
        pos.x = Mathf.Cos(radian) * orbitRadius;
        pos.z = Mathf.Sin(radian) * orbitRadius;

        cachedTransform.position = pos;
    }
}

/*
 using UnityEngine;

public class FloatingIsland : MonoBehaviour
{
    [Header("Rotating")]
    public float rotatingSpeed = 5f;

    [Header("Bobbing")]
    public float bobTimerSpeed = 0.2f;
    public float bobMagnitude = 2f;

    float startPosY;
    float timer;

    float orbitRadius;
    float currentAngle;

    void Start()
    {
        startPosY = transform.position.y;
        timer = Random.Range(0f, 1f);

        // Calculate the initial angle based on the object's starting position
        Vector3 pos = transform.position;
        currentAngle = Mathf.Atan2(pos.z, pos.x) * Mathf.Rad2Deg;

        // Ensure orbit radius matches the starting position
        orbitRadius = pos.magnitude;
    }

    void FixedUpdate()
    {
        // Bobbing
        timer += Time.fixedDeltaTime * bobTimerSpeed;
        float y = Mathf.Sin(timer) * bobMagnitude;

        // Rotation
        currentAngle += rotatingSpeed * Time.fixedDeltaTime;
        currentAngle %= 360;

        float radian = Mathf.Deg2Rad * currentAngle;
        float x = Mathf.Cos(radian) * orbitRadius;
        float z = Mathf.Sin(radian) * orbitRadius;

        transform.position = new Vector3(x, y, z);
    }
}

 
 */
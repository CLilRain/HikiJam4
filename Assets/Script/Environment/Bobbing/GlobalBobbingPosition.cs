using UnityEngine;

public class GlobalBobbingPosition : MonoBehaviour
{
    private static Vector3 position = Vector3.zero;
    public static Vector3 Position => position;

    private static Quaternion rotation = Quaternion.identity;
    public static Quaternion Rotation => rotation;

    public float sinValue;
    public float normalizedValue;

    [Header("Rotating")]
    public float rotatingAmount = 2f;

    [Header("Bobbing")]
    public float bobTimerSpeed = 0.5f;
    public float bobMagnitude = 0.5f;

    float timer;

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime * bobTimerSpeed;
        if (timer > Mathf.Rad2Deg)
        {
            timer -= Mathf.Rad2Deg;
        }

        var sin = Mathf.Sin(timer);

        // Rotating
        rotation = Quaternion.Euler(0f, timer * rotatingAmount * 360, 0f);
        // Bobbing


        position.y = sin * bobMagnitude;
    }
}
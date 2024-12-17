using UnityEngine;

public class GlobalBobbingPosition : MonoBehaviour
{
    public static Vector3 Position { get; private set; } = Vector3.zero;
    public static Quaternion Rotation { get; private set; } = Quaternion.identity;

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
        Rotation = Quaternion.Euler(0f, timer * rotatingAmount * 360, 0f);
        // Bobbing


        var pos = Position;
        pos.y = sin * bobMagnitude;
        Position = pos;
    }
}
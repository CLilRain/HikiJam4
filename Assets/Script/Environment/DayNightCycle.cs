using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("lerp speed")]
    public float normalSpeed = 1f;
    public float fastSpeed = 10f;

    Renderer rend;
    Vector2 offset;

    float timer;
    float currentSpeed;

    const string Offset = "_Offset";

    void Awake()
    {
        Reset();
        rend = GetComponent<Renderer>();
    }

    private void Reset()
    {
        currentSpeed = normalSpeed;
    }

    void Update()
    {
        OnSkyUpdate();
    }

    void OnSkyUpdate()
    {
        if (timer < 1f)
        {
            timer += Time.deltaTime * currentSpeed;
            SetValue(timer);
        }

        if (timer >= 1f)
        {
            timer -= 1f;
            currentSpeed = normalSpeed;
        }
    }

    void SetValue(float value)
    {
        offset.x = value;
        rend.sharedMaterial.SetVector(Offset, offset);
    }
}

using UnityEngine;

public class PlatformCloud : MonoBehaviour
{
    public float bobTimerSpeed = 0.2f;
    public float bobMagnitude = 1f;

    public float opaqueDuration = 2f;
    public float fadeDuration = 5f;

    float timer;

    float startPosY;
    Vector3 startPos;
    Vector3 offset;

    PlatformCloudPool pool;
    Transform cachedTransform;
    Renderer cachedRenderer;
    MaterialPropertyBlock propBlock;

    private void Awake()
    {
        cachedTransform = transform;
        cachedRenderer = GetComponent<Renderer>();

        propBlock = new MaterialPropertyBlock();
        cachedRenderer.GetPropertyBlock(propBlock);
    }

    public void Initialize(PlatformCloudPool pool)
    {
        this.pool = pool;
    }

    public void Show(Vector3 spawnPos)
    {
        SetAlpha(1f);
        
        startPos = spawnPos;
        startPosY = spawnPos.y;

        timer = 0f;
    }

    void Update()
    {
        timer += Time.fixedDeltaTime * bobTimerSpeed;
        if (timer > Mathf.Rad2Deg)
        {
            timer -= Mathf.Rad2Deg;
        }
        offset.y = Mathf.Sin(timer) * bobMagnitude;

        cachedTransform.position = startPos + offset;


        // Fade
        if (timer > opaqueDuration)
        {
            float fadeTimer = timer - opaqueDuration;
            float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);
            SetAlpha(alpha);
        }

        // Destroy
        if (timer > fadeDuration + opaqueDuration)
        {
            ReturnToPool();
        }
    }

    void SetAlpha(float alpha)
    {
        propBlock.SetFloat("_Alpha", alpha);
        cachedRenderer.SetPropertyBlock(propBlock);
    }

    void ReturnToPool()
    {
        timer = 0f;
        pool.ReturnToPool(this);
    }
}

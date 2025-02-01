using System.Collections;
using UnityEngine;

public class ScaleUpExpander : MonoBehaviour
{
    [SerializeField]
    private float duration = 5f;

    [SerializeField]
    private bool startHidden = true;

    [SerializeField]
    private Vector3 targetScale = Vector3.one;

    private Renderer rendererRef;
    private Vector3 startScale = Vector3.zero;
    private bool played;

    private void Awake()
    {
        if (rendererRef == null)
        {
            rendererRef = GetComponent<Renderer>();
        }

        if (startHidden)
        {
            transform.localScale = Vector3.zero;
            Hide();
        }
    }

    public void Play()
    {
        Debug.Log($"[ScaleUpExpander.Play]");

        if (!played)
        {
            played = true;
            StartCoroutine(RewardCoroutine());
        }
    }

    private IEnumerator RewardCoroutine()
    {
        Show();

        float t = 0f;

        while (t < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }

    private void Show()
    {
        if (rendererRef != null)
        {
            rendererRef.enabled = true;
        }
    }

    private void Hide()
    {
        if (rendererRef != null)
        {
            rendererRef.enabled = false;
        }
    }
}
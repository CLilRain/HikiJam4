using System.Collections;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance;

    [SerializeField]
    protected AudioSource audioSource;

    [Header("Gameplay")]
    [SerializeField]
    protected AudioClip pickupSound;

    [SerializeField]
    protected AudioClip winningSound;

    [SerializeField]
    protected AudioClip torch;

    [Header("UI")]
    [SerializeField]
    protected AudioClip UIClick;

    [SerializeField]
    protected AudioClip UIClickEcho;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayPickupSound()
    {
        audioSource.PlayOneShot(pickupSound);
    }

    public void PlayWinningSound()
    {
        audioSource.PlayOneShot(winningSound);
    }
    public void PlayTorchSound()
    {
        audioSource.PlayOneShot(torch);
    }


    public void PlayUIClick()
    {
        audioSource.PlayOneShot(UIClick);
    }

    public void PlayUIClickEcho()
    {
        audioSource.PlayOneShot(UIClickEcho);
    }
}
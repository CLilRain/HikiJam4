using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Torch : Weapon
{
    [Header("References")]
    [SerializeField]
    private Renderer torchTipRenderer;

    [SerializeField]
    private ParticleSystem particles;

    [Header("Settings")]
    [SerializeField]
    private Color litColor;

    [SerializeField]
    private Color dimColor;

    [Space]
    [SerializeField]
    private float maxFlameCharge = 60f;

    [SerializeField]
    private float chargeLossPerSecond = 1f;

    [Space]
    [SerializeField]
    private float maxPfxSize;

    [SerializeField]
    private float minPfxSize;

    MainModule mainModule;
    Material tornMaterial;
    float flameCharge;
    bool isActive;

    void Awake()
    {
        tornMaterial = torchTipRenderer.material;
        mainModule = particles.main;
    }

    void Update()
    {
        if (isActive && flameCharge > Mathf.Epsilon)
        {
            flameCharge -= Time.deltaTime * chargeLossPerSecond;
            SetLitAmount(flameCharge / maxFlameCharge);

            if (flameCharge <= Mathf.Epsilon)
            {
                flameCharge = 0f;
                DisableEffect();
            }
        }
    }

    public override void DisableEffect()
    {
        SetLitAmount(0f);

        isActive = false;
        particles.Stop();
        torchTipRenderer.enabled = false;
    }

    public override void EnableEffect()
    {
        isActive = true;
        particles.Play();
        torchTipRenderer.enabled = true;

        flameCharge = maxFlameCharge;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameSettings.Instance.playerWeaponLayer.Contains(other.gameObject))
        {
            EnableEffect();
        }
    }

    void SetLitAmount(float percent)
    {
        tornMaterial.color = Color.Lerp(dimColor, litColor, percent);

        mainModule.startSize = Mathf.Lerp(minPfxSize, maxPfxSize, percent);
    }
}
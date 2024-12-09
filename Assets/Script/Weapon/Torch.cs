﻿using System.Collections;
using UnityEngine;

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

    ParticleSystem.MainModule mainModule;
    Material tornMaterial;
    float flameCharge;
    bool isActive;

    protected override void Awake()
    {
        base.Awake();
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
                Disable();
            }
        }
    }

    public override void Enable(Agent owner)
    {
        this.owner = owner;
    }

    public override void Disable()
    {
        owner = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameSettings.Instance.playerWeaponLayer.Contains(other.gameObject))
        {
        }
    }

    public void FullyRecharge()
    {
        SetLitAmount(1f);
    }

    void SetLitAmount(float percent)
    {
        tornMaterial.color = Color.Lerp(dimColor, litColor, percent);

        mainModule.startSize = Mathf.Lerp(minPfxSize, maxPfxSize, percent);
    }

    public override void Interact(Agent player)
    {
        base.Interact(player);
        player.PickUpWeapon(this);
    }
}
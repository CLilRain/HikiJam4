﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Player detected item info")]
    public TMP_Text detectedLabel;
    public Image detectedBacground;

    [Header("Essence")]
    public TMP_Text essenceText;
    public Animator essenceAnimator;

    [Header("Items")]
    public Image router;
    public Image waterPot;
    public Image riceBag;
    
    [Header("Essence")]
    public DialogueManager dialogueManager;

    public bool InDialogue => dialogueManager.InDialogue;

    private void Awake()
    {
        Instance = this;
        ClearDetectedInfo();

        router.enabled = false;
        waterPot.enabled = false;
        riceBag.enabled = false;
    }

    public void SetEssence(int amount)
    {
        essenceText.text = amount.ToString();
        //essenceAnimator.Play("Bounce", 0, 0f);
    }

    public void UpdateItemDisplay(ItemTypes itemType, bool isShown)
    {
        switch (itemType)
        {
            case ItemTypes.Router:
                router.enabled = isShown;
                break;
            case ItemTypes.WaterPot:
                waterPot.enabled = isShown;
                break;
            case ItemTypes.RiceBag:
                riceBag.enabled = isShown;
                break;
            default:
                break;
        }
    }

    public void SetDetectedInfo(string objectName)
    {
        detectedBacground.enabled = true;
        detectedLabel.text = objectName;
    }   
    
    public void ClearDetectedInfo()
    {
        detectedBacground.enabled = false;
        detectedLabel.text = string.Empty;
    }

    public void SetDialogue(DialogueSet dialogue)
    {
        if (!dialogueManager.InDialogue)
        {
            ClearDetectedInfo();
            dialogueManager.StartDialogue(dialogue);
        }
    }
}
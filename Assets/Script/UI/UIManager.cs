using UnityEngine;
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

    public void PickedUpItem(ItemTypes itemType)
    {
        switch (itemType)
        {
            case ItemTypes.Router:
                router.enabled = true;
                break;
            case ItemTypes.WaterPot:
                waterPot.enabled = true;
                break;
            case ItemTypes.RiceBag:
                riceBag.enabled = true;
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
}
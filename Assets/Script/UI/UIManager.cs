using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Player detected item info")]
    public TMP_Text detectedLabel;
    public Image detectedBacground;

    [Header("Essecen")]
    public TMP_Text essenceText;
    public Animator essenceAnimator;


    private void Awake()
    {
        Instance = this;
    }


    public void SetEssence(int amount)
    {
        essenceText.text = amount.ToString();
        //essenceAnimator.Play("Bounce", 0, 0f);
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
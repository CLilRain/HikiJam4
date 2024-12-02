using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

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

}
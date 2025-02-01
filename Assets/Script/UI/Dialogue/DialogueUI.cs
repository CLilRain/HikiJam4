using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public TextMeshProUGUI speakerTextBox;
    public TextMeshProUGUI dialogueTextBox;
    public Image BG;

    [Space]
    public float charDisplayDelay = 0.08f;

    private Coroutine coroutine;
    private string line;
    private Action onComplete;

    private void Start()
    {
        HideDialogue();
    }

    public void ShowDialogue(string speakerName, string line, Action onComplete = null)
    {
        this.line = line;
        this.onComplete = onComplete;

        HideDialogue();

        speakerTextBox.text = speakerName;
        dialogueTextBox.text = line;
        BG.enabled = true;

        //StopCoroutine();
        //coroutine = StartCoroutine(TypeLine());
    }

    public void HideDialogue()
    {
        speakerTextBox.text = string.Empty;
        dialogueTextBox.text = string.Empty;
        BG.enabled = false;

        StopCoroutine();
    }

    private void StopCoroutine()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in line.ToCharArray())
        {
            dialogueTextBox.text += c;
            yield return new WaitForSeconds(charDisplayDelay);
        }

        if (onComplete != null)
        {
            onComplete.Invoke();
        }
    }
}

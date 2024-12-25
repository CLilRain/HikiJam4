using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueTextBox;
    public TextMeshProUGUI speakerTextBox;
    public Image background;

    [Space]
    public float characterDisplayDelay = 0.08f;

    private int index;
    NPC npc;
    Coroutine dialogueCoroutine;

    public bool InDialogue => dialogueCoroutine != null;

    private void Start()
    {
        Disable();
    }

    public void StartDialogue(NPC npc)
    {
        if (dialogueCoroutine != null)
        {
            return;
        }

        this.npc = npc;
        Enable();

        dialogueCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        while (index < dialogue.lines.Length)
        {
            // Display line
            foreach (char c in dialogue.lines[index].ToCharArray())
            {
                dialogueTextBox.text += c;
                yield return new WaitForSeconds(characterDisplayDelay);
            }

            // Wait for input
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Return)
                    || Input.GetKeyDown(KeyCode.E)
                    || Input.GetMouseButtonDown(0))
                {
                    NextLine();
                    break;
                }
            }


            if (index >= dialogue.lines.Length)
            {
                DialogueCompleted();
                yield break;
            }
        }
    }

    void DialogueCompleted()
    {
        dialogueCoroutine = null;
        if (onComplete != null)
        {
            onComplete.Invoke();
            onComplete = null;
        }

        Disable();
    }

    void NextLine()
    {
        index++;
        dialogueTextBox.text = string.Empty;
    }

    public void Enable()
    {
        background.enabled = true;
        Reset();
    }

    public void Disable()
    {
        Reset();
        background.enabled = false;
    }

    private void Reset()
    { 
        speakerTextBox.text = string.Empty;
        dialogueTextBox.text = string.Empty;
        index = 0;
    }
}

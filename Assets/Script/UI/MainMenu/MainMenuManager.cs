using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Animator cameraAnimator;
    public CanvasGroup canvasGroup;
    public float delayBeforeLoadGame = 1f;
    public float canvasFadeDuration = 0.5f;

    bool interactable = true;

    public void StartGame()
    {
        if (interactable)
        {
            interactable = false;
            SfxManager.Instance.PlayUIClickEcho();

            StartCoroutine(StartGameCoroutine());
            StartCoroutine(FadeOutUI());
        }
    }

    IEnumerator StartGameCoroutine()
    {
        cameraAnimator.Play("Zoom");
        yield return new WaitForSeconds(delayBeforeLoadGame);
        SceneManager.LoadScene(1);
    }

    IEnumerator FadeOutUI()
    {
        float t = 0f;
        while (t < canvasFadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = 1f - (t / canvasFadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}

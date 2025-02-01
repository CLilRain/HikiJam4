using System.Collections;
using UnityEngine;

public enum GameState
{
    Loading,
    Playing,
    Conversation,
    GameOver
}

public class GameStateController
{
    private GameManager gm;
    private Coroutine transition;

    private const float TransitionDelay = 0.2f;

    public GameState GameState { get; private set; } = GameState.Loading;

    public GameStateController(GameManager gm)
    {
        this.gm = gm;    
    }

    public void GoToGameState(GameState state, bool immediately = false)
    {
        StopTransitionCoroutine();

        if (immediately)
        {
            GoToState(state);
        }
        else
        {
            transition = gm.StartCoroutine(TransitionCoroutine(state));
        }
    }

    private IEnumerator TransitionCoroutine(GameState state)
    {
        float t = 0f;
        while (t < TransitionDelay)
        {
            t += Time.deltaTime;
            yield return null;
        }

        GoToState(state);
    }

    private void GoToState(GameState state)
    {
        Debug.Log($"<color=orange> Entered State {GameState} </color>");
        GameState = state;
    }

    private void StopTransitionCoroutine()
    {
        if (transition != null)
        {
            gm.StopCoroutine(transition);
            transition = null;
        }
    }
}

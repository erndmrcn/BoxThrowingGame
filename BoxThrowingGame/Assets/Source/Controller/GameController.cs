using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public BoxController boxController;
    public ScreenManager screenManager;
    public PlayerController playerController;
    public static GameController Manager;
    public int PlayerScore;
    public enum Gamestates { ready, game, pause, end };
    public Gamestates CurrentState;
    public float GameTime;

    // check float time = Time.realtimeSinceStartup; == 2 min or not
    // can add timer on gamescreen

    private void Awake()
    {
        if (Manager == null)
        {
            Manager = this;
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        playerController.Initialize();
        screenManager.Initialize();
        boxController.Initialize();
    }

    public void onScoreUpdate(int score)
    {
        PlayerScore += score;
    }

    public void changeState(Gamestates state)
    {
        CurrentState = state;
        switch (state)
        {
            case Gamestates.ready:
                onReadyState();
                break;
            case Gamestates.game:
                onGameState();
                break;
            case Gamestates.pause:
                onPauseState();
                break;
            case Gamestates.end:
                onEndState();
                break;
            default:
                break;
        }
    }

    private void onReadyState()
    {
        GameTime = 45;
        PlayerScore = 0;
        ScreenManager.Manager.showScreen(0);
    }

    private void onGameState()
    {
        ScreenManager.Manager.showScreen(1);
    }

    private void onPauseState()
    {
        PlayerController.Manager.ResetPhysic();
        ScreenManager.Manager.showScreen(2);
    }

    private void onEndState()
    {
        ScreenManager.Manager.showScreen(3);
    }

    private void Update()
    {
        if (CurrentState == Gamestates.game)
        {
            GameTime -= Time.deltaTime;
            ((GameScreen)ScreenManager.Manager.Screens[1]).UpdateTime(GameTime);
            ((GameScreen)ScreenManager.Manager.Screens[1]).UpdateScore(PlayerScore);
            if (GameTime <= 0.0f)
            {
                changeState(Gamestates.end);
            }
            PlayerController.Manager.ControllerUpdate();
        }
    }
}

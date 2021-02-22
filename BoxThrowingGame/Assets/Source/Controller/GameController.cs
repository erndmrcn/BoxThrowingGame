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
    public enum Gamestates { ready, game, pause, end, resume };
    public Gamestates CurrentState;
    public float GameTime;

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
        GameTime = 120;
    }

    public void onScoreUpdate(int score)
    {
        PlayerScore += score;
    }

    public void changeState(Gamestates state)
    {
        CurrentState = state;
        switch (CurrentState)
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
            case Gamestates.resume:
                onResumeState();
                break;
            case Gamestates.end:
                onEndState();
                break;
            default:
                break;
        }
    }

    private void Restart()
    {
        PoolingController.PoolingManager.cleanScene();
        BoxController.Manager.spawn();
        PlayerController.Manager.Reposition();
        GameTime = 120;
        PlayerScore = 0;
    }

    private void onReadyState()
    {
        ScreenManager.Manager.showScreen(0);
    }

    private void onGameState()
    {
        Restart();
        ScreenManager.Manager.showScreen(1);
    }

    private void onResumeState()
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
        PoolingController.PoolingManager.cleanScene();
        PlayerController.Manager.Reposition();
        PlayerController.Manager.ResetPhysic();
        ScreenManager.Manager.showScreen(3);
    }

    private void Update()
    {
        
        if (CurrentState == Gamestates.ready)
        {
            ((MainScreen)ScreenManager.Manager.Screens[0]).BoxMassUpdate();
            ((MainScreen)ScreenManager.Manager.Screens[0]).SpeedSliderUpdate();
            ((MainScreen)ScreenManager.Manager.Screens[0]).JumpSliderUpdate();
            ((MainScreen)ScreenManager.Manager.Screens[0]).PowerSliderUpdate();
        }
        if (CurrentState == Gamestates.game || CurrentState == Gamestates.resume)
        {
            
            GameTime -= Time.deltaTime;
            ((GameScreen)ScreenManager.Manager.Screens[1]).UpdateTime(GameTime);
            ((GameScreen)ScreenManager.Manager.Screens[1]).UpdateScore(PlayerScore);
            ((GameScreen)ScreenManager.Manager.Screens[1]).ControllerUpdate();
            if (GameTime <= 0.0f)
            {
                //PlayerController.Manager.ResetPhysic();
                changeState(Gamestates.end);
            }
            PlayerController.Manager.ControllerUpdate();
            
        }
        if (CurrentState == Gamestates.end)
        {
            ((WinScreen)ScreenManager.Manager.Screens[3]).ShowScore(PlayerScore);
        }
    }
}

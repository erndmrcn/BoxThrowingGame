using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : Screen
{
    public Button restartButton;
    public Button mainMenuButton;
    public Button resumeButton;

    public override void Initialize()
    {
        base.Initialize();
    }

    public void mainButtonEvent()
    {
        // goto main menu
        GameController.Manager.changeState(GameController.Gamestates.ready);
    }

    public void resumeButtonEvent()
    {
        // resume where you left
        // add another state that cont. where the player left
        GameController.Manager.changeState(GameController.Gamestates.game);
    }

    public void restartButtonEvent()
    {
        // restart (reset the game)
        GameController.Manager.changeState(GameController.Gamestates.ready);
    }

}

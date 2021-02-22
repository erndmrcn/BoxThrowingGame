using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : Screen
{
    public Button mainMenuButton;
    public Button tryAgainButton;
    public Text txt_score;

    public override void Initialize()
    {
        base.Initialize();
    }

    public void mainMenuButtonEvent()
    {
        GameController.Manager.changeState(GameController.Gamestates.ready);
    }

    public void ShowScore(int PlayerScore)
    {
        txt_score.text = PlayerScore.ToString();
    }

    public void tryAgainButtonEvent()
    {
        // restart (reset) the game
        GameController.Manager.changeState(GameController.Gamestates.game);
    }
}

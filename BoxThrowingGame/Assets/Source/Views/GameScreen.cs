using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : Screen
{
    public Text txt_score;
    public Text txt_time;

    public override void Initialize()
    {
        base.Initialize();
    }
    public void EscapeEvent()
    {
        // show pause screen
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameController.Manager.changeState(GameController.Gamestates.pause);
        }
    }

    public void UpdateTime(float time)
    {
        txt_time.text = time.ToString();
    }

    public void UpdateScore(int PlayerScore)
    {
        txt_score.text = PlayerScore.ToString();
    }
    private void Update()
    {
        EscapeEvent();
    }
}

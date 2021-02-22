using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : Screen
{
    public Text txt_score;
    public Text txt_time;
    public float PowerCoeff;
    public bool KickPressing;
    public Image KickBar;
    public override void Initialize()
    {
        base.Initialize();
        PowerCoeff = 1000.0f;
        KickBar.fillAmount = 0;
    }
    public void EscapeEvent()
    {
        // show pause screen
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            GameController.Manager.changeState(GameController.Gamestates.pause);
        }
    }

    public void ControllerUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnKickDown();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            OnKickUp();
        }
    }

    public void OnKickDown()
    {
        // stack kick power 
        // controls power bar image as well
        PlayerController.Manager.StackedKickPower = PlayerController.Manager.kickpower;
        KickPressing = true;
    }
    public void OnKickUp()
    {
        // apply kick
        KickPressing = false;
        PlayerController.Manager.KickAnimationOn();
    }
    public void OnKickExit()
    {
        // appliy kick with minimum value
        KickPressing = false;
        PlayerController.Manager.KickAnimationOn();
    }
    public void OnJumpDown()
    {
        // wait for it to be up to jump
        PlayerController.Manager.JumpAnimationOn();
    }

    public void OnJumpExit()
    {
        // apply jump
        PlayerController.Manager.JumpAnimationOn();
    }
    public void UpdateTime(float time)
    {
        txt_time.text = time.ToString("0.0");
    }

    public void UpdateScore(int PlayerScore)
    {
        txt_score.text = PlayerScore.ToString();
    }
    private void Update()
    {
        EscapeEvent();
        if (KickPressing)
        {
            if (PlayerController.Manager.StackedKickPower <= PlayerController.Manager.MaxKickPower)
            {
                PlayerController.Manager.StackedKickPower += Time.deltaTime * PowerCoeff;
            }
            KickBar.fillAmount = PlayerController.Manager.StackedKickPower / PlayerController.Manager.MaxKickPower;
        }
        else if (!KickPressing)
        {
            KickBar.fillAmount = 0;
        }
    }
}

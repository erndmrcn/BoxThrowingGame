using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainScreen : Screen
{
    // buttons
    // show
    // disable
    // hide can inheret from screen 
    public Button startButton;
    public Slider PowerSlider;
    public Slider SpeedSlider;
    public Slider JumpValueSlider;

    public override void Initialize()
    {
        base.Initialize();
        SpeedSlider.minValue = 1.0f;
        SpeedSlider.value = 10.0f;
        SpeedSlider.maxValue = 50.0f;
        JumpValueSlider.minValue = 100.0f;
        JumpValueSlider.value = 300.0f;
        JumpValueSlider.maxValue = 250.0f;
    }

    public void StartTaskOnClick()
    {
        GameController.Manager.changeState(GameController.Gamestates.game);
    }

    public void PowerSliderUpdate()
    {
        PlayerController.Manager.UpdatePower(PowerSlider.value);
        //SpeedSlider.maxValue = 100.0f;
    }

    public void SpeedSliderUpdate()
    {
        PlayerController.Manager.UpdatePlayerSpeed(SpeedSlider.value);
    }

    public void JumpValueSliderUpdate()
    {
        PlayerController.Manager.UpdateJumpValue(JumpValueSlider.value);
    }

}

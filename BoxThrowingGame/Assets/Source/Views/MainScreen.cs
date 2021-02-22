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
    public Slider JumpSlider;
    public Slider MassSlider;

    public override void Initialize()
    {
        base.Initialize();
        // player speed
        SpeedSlider.minValue = 1.0f;
        SpeedSlider.value = 3.0f;
        SpeedSlider.maxValue = 10.0f;
        // kick power
        PowerSlider.minValue = 100.0f;
        PowerSlider.value = 500.0f;
        PowerSlider.maxValue = 2000.0f;
        // jump value
        JumpSlider.minValue = 1.0f;
        JumpSlider.value = 2.0f;
        JumpSlider.maxValue = 20.0f;
        // box mass
        MassSlider.minValue = 1.0f;
        MassSlider.value = 2.0f;
        MassSlider.maxValue = 20.0f;
    }

    public void StartTaskOnClick()
    {
        GameController.Manager.changeState(GameController.Gamestates.game);
    }

    public void BoxMassUpdate()
    {
        PoolingController.PoolingManager.UpdateWeight(MassSlider.value);
    }

    public void JumpSliderUpdate()
    {
        PlayerController.Manager.UpdateJumpValue(JumpSlider.value);
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

}

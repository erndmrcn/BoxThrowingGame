using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    // 1 -> mainmenu screen
    // 2 -> game screen
    // 3 -> pause screen
    // 4 -> final screen
    public int state;
    public static ScreenManager Manager;
    public Screen ActiveScreen;
    public List<Screen> Screens;

    public void Initialize()
    {
        if (Manager == null)
        {
            Manager = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        foreach (Screen obj in Screens)
        {
            obj.Initialize();
        }

        ActiveScreen = Screens[0];
        ActiveScreen.show();
    }

    public void showScreen(int index)
    {
        ActiveScreen.hide();
        ActiveScreen = Screens[index];
        ActiveScreen.show();
    }


}

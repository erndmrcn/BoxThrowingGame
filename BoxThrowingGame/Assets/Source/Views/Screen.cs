using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    public Animator Animator;

    public virtual void Initialize()
    {
        disable();
    }
    // show current screen
    public virtual void show()
    {
        // make objects that are in the screen visible 
        if (Animator != null)
        {
            Animator.Play("Intro", 0, 0);
        }
        gameObject.SetActive(true);

    }

    // hide current screen to show another
    public virtual void hide()
    {
        if (Animator != null)
        {
            Animator.Play("Outro", 0, 0);
        }
        else
        {
            disable();
        }
    }

    // disable objects that are in the screen that is hidden
    public virtual void disable()
    {
        gameObject.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonScript : MonoBehaviour
{
    public Animator anim;

    public void ButtonIsPressed()
    {
        anim.SetTrigger("IsPressed");
    }
    
    public void Quit()
    {
        SceneLoader.instance.QuitGame();
    }
}

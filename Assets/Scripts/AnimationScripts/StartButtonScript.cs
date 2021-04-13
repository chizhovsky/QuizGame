using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonScript : MonoBehaviour
{
    public Animator anim;

    public void ButtonIsPressed()
    {
        anim.SetTrigger("IsPressed");
    }
    public void StartGame()
    {
        SceneLoader.instance.StartGame();
    }
}

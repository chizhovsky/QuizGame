using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButtonScript : MonoBehaviour
{
    public Animator anim;

    public GameObject menuPanel;
    public GameObject settingsPanel;
    public void ButtonIsPressed()
    {
        anim.SetTrigger("IsPressed");
    }

    public void OpenSettingsPanel()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
}

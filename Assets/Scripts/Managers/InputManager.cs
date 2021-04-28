using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager 
{
    private static readonly InputManager _instance = new InputManager();
    static InputManager(){}
    private InputManager(){}
    public static InputManager Instance
    {
        get { return _instance;}
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (UIManager.Instance.settingsMenu.GetComponent<CanvasGroup>().alpha == 1)
            {
                UIManager.Instance.mainMenu.ShowMenu();
                UIManager.Instance.settingsMenu.HideMenu();
                AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.audioData.assPressed.clip);
            }
        }
    }
}

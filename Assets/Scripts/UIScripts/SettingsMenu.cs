using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : Menu
{
    [SerializeField] private Button _backToMenuButton;
    public void Init()
    {
        _backToMenuButton.onClick.AddListener(BackToMenu);
    }
    private void BackToMenu()
    {
        UIManager.Instance.mainMenu.ShowMenu();
        UIManager.Instance.settingsMenu.HideMenu();        
    }
}

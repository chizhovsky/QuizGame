using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Animator _startButtonAnim;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Animator _settingsButtonAnim;

    public void Init()
    {
        _startButton.onClick.AddListener(StartOnClick);
        _settingsButton.onClick.AddListener(SettingsOnClick);
    }
    private void StartOnClick()
    {
        StartCoroutine(StartAnimRoutine());
    }
    private void SettingsOnClick()
    {
        StartCoroutine(SettingsAnimRoutine());        
    } 
    IEnumerator StartAnimRoutine()
    {
        _startButtonAnim.SetTrigger("IsPressed");
        yield return new WaitForSeconds(2f);
        UIManager.Instance.loadingMenu.ShowMenu();
        UIManager.Instance.mainMenu.HideMenu();
        GameplayManager.Instance.LoadDataFromWeb();
    }
    IEnumerator SettingsAnimRoutine()
    {
        _settingsButtonAnim.SetTrigger("IsPressed");
        yield return new WaitForSeconds(2f);
        UIManager.Instance.settingsMenu.ShowMenu();
        UIManager.Instance.mainMenu.HideMenu();
    }
}

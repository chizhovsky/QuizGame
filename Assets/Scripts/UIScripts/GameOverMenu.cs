using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : Menu
{
    public Text finalScoreText;
    public Text recordText;
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _exitToMenuButton;

    public void Init()
    {
        _retryButton.onClick.AddListener(RetryPressed);
        _exitToMenuButton.onClick.AddListener(ExitPressed);
    }

    private void RetryPressed()
    {
        StartCoroutine(RetryPressedRoutine());
    }

    private void ExitPressed()
    {
        UIManager.Instance.mainMenu.ShowMenu();
        UIManager.Instance.gameOverMenu.HideMenu();
    }

    private IEnumerator RetryPressedRoutine()
    {
        UIManager.Instance.loadingMenu.ShowMenu();
        UIManager.Instance.gameOverMenu.HideMenu();
        yield return new WaitForSeconds(2);
        GameplayManager.Instance.LoadDataFromWeb();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManager : MonoSingleton<SceneManager>
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    public void LoadMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }
    public void ResetRecord()
    {
        PlayerPrefs.SetInt("Highscore", 0);
        Debug.Log("Сброс рекорда, PlayerPrefs: " + PlayerPrefs.GetInt("Highscore"));
    }
    public void QuitGame()
    {
        {
            Application.Quit();
            Debug.Log("Вышел из игры");
        }
    }
}

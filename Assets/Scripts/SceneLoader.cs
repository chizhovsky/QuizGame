using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    public static SceneLoader instance { get; private set; }
    private void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("StartMenu");
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

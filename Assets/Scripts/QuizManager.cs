﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking ;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> listOfQuestions;
    public QuestionsList questionsList;
    public GameObject[] options;

    public Text questionText;
    public Text scoreText;
    public Text finalScoreText;
    public Text recordText;
    public CountdownTimer countdownTimer;
    public GameObject gamePanel;
    public GameObject gameOverPanel;
    public GameObject loadingScreen;
    public RawImage questionImage;

    public int currentQuestion;
    public int score;
    private int questionCounter;


    private void Start()
    {
        StartCoroutine (GetJsonData ("https://drive.google.com/uc?export=download&id=1NBge4o01xtKiWIovMMIFtQEriG-WIIAN")) ;
        gamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    IEnumerator GetJsonData (string url) 
    {
        UnityWebRequest request = UnityWebRequest.Get (url);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
            {
            Debug.Log ("Can't load questions");
            } 
        else 
        {
            QuestionsList loadedData = JsonUtility.FromJson<QuestionsList> (request.downloadHandler.text) ;
            questionsList = loadedData;
            listOfQuestions = questionsList.questionAndAnswers;
            Debug.Log ("Questions are loaded from web");
            if (request.isDone)
            {
                GenerateQuestion();
                ResetTimer();
                gamePanel.SetActive(true);
                loadingScreen.SetActive(false);
            }
            else 
            {
                loadingScreen.SetActive(true);
                gamePanel.SetActive(false);
            }
        }

        request.Dispose () ;
    }
    
    IEnumerator GetImage (string url) 
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture (url) ;
        yield return request.SendWebRequest() ;

        if (request.isNetworkError || request.isHttpError) 
            {
            Debug.Log("Can't load image");
            } 
        else 
            {
            questionImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture ;
            }

        request.Dispose () ;
    }

    public void NextQuestion()
    {
        listOfQuestions.RemoveAt(currentQuestion);
        ResetTimer(); 
        GenerateQuestion();
        questionCounter++;
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = listOfQuestions[currentQuestion].answers[i];
       
        if (listOfQuestions[currentQuestion].correctAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void GenerateQuestion()
    {
        if (questionCounter < 5)
        {
            currentQuestion = Random.Range(0, listOfQuestions.Count);
            questionText.text = listOfQuestions[currentQuestion].question;
            StartCoroutine (GetImage (listOfQuestions[currentQuestion].imageUrl));
            SetAnswers();
        }
        else
        {
            GameOver();
        }
    }

    public void ResetTimer()
    {
        countdownTimer.currentTime = 10f;
    }

    void GameOver()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Твой результат - " + score;
        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
            recordText.text = "Поздравляем! Это твой новый рекорд!";
        }
        else
        {
            recordText.text = "Твой рекорд - " + PlayerPrefs.GetInt("Highscore");
        }

        Debug.Log("PlayerPrefs: " + PlayerPrefs.GetInt("Highscore"));
    }

    public int CountPoints(float time)
    {
        string str = time.ToString("0.0");
        time = float.Parse(str);
        if (time > 9.5)
            score += 10;
        else if (time <= 9.5 && time > 9.0)
            score += 9;
        else if (time <= 9.0 && time > 8.5)
            score += 8;
        else if (time <= 8.5 && time > 8.0)
            score += 7;
        else if (time <= 8.0 && time > 7.0)
            score += 6;
        else if (time <= 7.0 && time > 6.0)
            score += 5;
        else if (time <= 6.0 && time > 5.0)
            score += 4;
        else if (time <= 5.0 && time > 4.0)
            score += 3;
        else if (time <= 4.0 && time > 3.0)
            score += 2;
        else if (time <= 3.0 && time > 0.1)
            score += 1;
        else 
            Debug.Log("You gained 0 points");
        return score;
    }
}

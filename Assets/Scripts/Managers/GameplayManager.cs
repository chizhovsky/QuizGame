using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class GameplayManager
{
    private static readonly GameplayManager _instance = new GameplayManager();
    static GameplayManager(){}
    private GameplayManager(){}
    public static GameplayManager Instance
    {
        get { return _instance;}
    }

    private List<QuestionAndAnswers> _listOfQuestions;
    private List<int> _questionsIdList = new List<int>();
    private List<string> _arrayOfImageURL = new List<string>();
    private List<Texture> _loadedImages = new List<Texture>();
    private QuestionsList _questionsList;
    public int currentQuestion;
    public int score;
    public int questionCounter = 0;
    public float currentTime; 

    public void Init()
    {

    }

    private IEnumerator GetJsonDataRoutine (string url) 
    {
        UnityWebRequest request = UnityWebRequest.Get (url);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log ("Can't load questions");
        } 
        else 
        {
            QuestionsList loadedData = JsonUtility.FromJson<QuestionsList> (request.downloadHandler.text);
            _questionsList = loadedData;
            _listOfQuestions = _questionsList.questionAndAnswers;
            Debug.Log ("Questions are loaded from web");
            GenerateListOfQuestions();
            GameManager.Instance.StartCoroutine (LoadQuestionsRoutine(_arrayOfImageURL));
        }
    }

    private IEnumerator LoadQuestionsRoutine (List<string> url) 
    {
        for (int i = 0; i < 6; i++)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture (url[i]);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError) 
            {
                Debug.Log("Can't load image");
            } 
            else 
            {    
                _loadedImages.Add(((DownloadHandlerTexture)request.downloadHandler).texture);
            }
        }
        GenerateQuestion();
        UIManager.Instance.gameMenu.ShowMenu();
        UIManager.Instance.loadingMenu.HideMenu();
        UIManager.Instance.gameMenu.enabled = true;
    }

    public void LoadDataFromWeb()
    {
        GameManager.Instance.StartCoroutine (GetJsonDataRoutine ("https://drive.google.com/uc?export=download&id=16jBimVuS6mQoVzUVUtoCT46vMo9OzqMB"));
    }

    private void SetAnswers()
    {
        for (int i = 0; i < UIManager.Instance.gameMenu.answerButtons.Length; i++)
        {
            UIManager.Instance.gameMenu.isCorrect[i] = false;
            UIManager.Instance.gameMenu.answerText[i].text = _listOfQuestions[currentQuestion].answers[i];
       
        if (_listOfQuestions[currentQuestion].correctAnswer == i+1)
            {
                UIManager.Instance.gameMenu.isCorrect[i] = true;                                
            }
        }
    }

    public void NextQuestion()
    {
        questionCounter++;
        ResetTimer(); 
        GenerateQuestion();
    }
    
    private void GenerateListOfQuestions()
    {
        for (int i = 0; i < 6; i++)
        {
            int myNumber = Random.Range (0, _listOfQuestions.Count);
            int newNumber = 0;
            if (!_questionsIdList.Contains(myNumber))
            {
                _questionsIdList.Add(myNumber);
            }
            else 
            {
                do
                {
                newNumber = Random.Range (0, _listOfQuestions.Count);
                } while (_questionsIdList.Contains(newNumber));
                _questionsIdList.Add(newNumber);
            }
            _arrayOfImageURL.Add(_listOfQuestions[_questionsIdList[i]].imageUrl);      
        }
    }
    
    private void GenerateQuestion()
    {
        if (questionCounter < 6)
        {
            currentQuestion = _questionsIdList[questionCounter];
            UIManager.Instance.gameMenu.questionText.text = _listOfQuestions[currentQuestion].question;
            UIManager.Instance.gameMenu.questionImage.texture = _loadedImages[questionCounter];
            SetAnswers();
        }
        else
        {
            GameOver();                
        }
    }

    public void ResetTimer()
    {
        currentTime = 10f;
    }

    private void GameOver()
    {
        UIManager.Instance.gameMenu.enabled = false;
        UIManager.Instance.gameOverMenu.ShowMenu();
        UIManager.Instance.gameMenu.HideMenu();
        UIManager.Instance.gameOverMenu.finalScoreText.text = "Твой результат - " + score;
        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
            UIManager.Instance.gameOverMenu.recordText.text = "Поздравляем! Это твой новый рекорд!";
        }
        else
        {
            UIManager.Instance.gameOverMenu.recordText.text = "Твой рекорд - " + PlayerPrefs.GetInt("Highscore");
        }
        Debug.Log("PlayerPrefs: " + PlayerPrefs.GetInt("Highscore")); 
        ResetRound();       
    }

    private void ResetRound()
    {
        _listOfQuestions.Clear();
        _questionsIdList.Clear();
        _arrayOfImageURL.Clear();
        _loadedImages.Clear();
        questionCounter = 0;
        score = 0;
        UIManager.Instance.gameMenu.scoreText.text = score.ToString();
        ResetTimer();
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
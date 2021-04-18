using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class QuizManager 
{
    private static readonly QuizManager _instance = new QuizManager();
    static QuizManager(){}
    private QuizManager(){}
    public static QuizManager Instance
    {
        get { return _instance;}
    }
    private List<QuestionAndAnswers> _listOfQuestions;
    private List<int> _questionsIdList = new List<int>();
    private List<string> _arrayOfImageURL = new List<string>();
    [HideInInspector] public GameObject[] options;
    public QuestionsList questionsList;
    public int currentQuestion;
    public int score;
    public int questionCounter = 0;

    public float currentTime;
    private UIManager UI;
    [HideInInspector] public Text[] answerText = new Text[4];    
    [HideInInspector] public Text questionText;
    [HideInInspector] public Text scoreText;
    [HideInInspector] public Text finalScoreText;
    [HideInInspector] public Text recordText;
    [HideInInspector] public Text questionCounterText;
    [HideInInspector] public GameObject menuPanel;
    [HideInInspector] public GameObject settingsPanel;
    [HideInInspector] public GameObject gamePanel;
    [HideInInspector] public GameObject gameLoadingPanel;
    [HideInInspector] public GameObject gameOverPanel;
    [HideInInspector] public List<Texture> loadedImages = new List<Texture>();
    [HideInInspector] public RawImage questionImage;
    [HideInInspector] public Text timerText;

    public void Update()
    {
        if (currentTime <= 0)
        {
            NextQuestion();
        }
    }
    public void Init()
    {
        UI.SwitchPanels(gameLoadingPanel, gamePanel);
        //StartCoroutine (GetJsonData ("https://drive.google.com/uc?export=download&id=1NBge4o01xtKiWIovMMIFtQEriG-WIIAN"));
        UI.SwitchPanels(gamePanel, gameOverPanel);
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
            QuestionsList loadedData = JsonUtility.FromJson<QuestionsList> (request.downloadHandler.text);
            questionsList = loadedData;
            _listOfQuestions = questionsList.questionAndAnswers;
            Debug.Log ("Questions are loaded from web");
            GenerateListOfQuestions();
           // StartCoroutine (LoadQuestions(_arrayOfImageURL));
        }
    }

    IEnumerator LoadQuestions (List<string> url) 
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
                loadedImages.Add(((DownloadHandlerTexture)request.downloadHandler).texture);
            }
        }
        GenerateQuestion();
        ResetTimer();
        UI.SwitchPanels(gamePanel, gameLoadingPanel);
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            answerText[i].text = _listOfQuestions[currentQuestion].answers[i];
       
        if (_listOfQuestions[currentQuestion].correctAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;                                
            }
        }
    }

    public void NextQuestion()
    {
        questionCounter++;
        ResetTimer(); 
        GenerateQuestion();
    }
    
    void GenerateListOfQuestions()
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
    
    void GenerateQuestion()
    {
        if (questionCounter < 6)
        {
            currentQuestion = _questionsIdList[questionCounter];
            questionText.text = _listOfQuestions[currentQuestion].question;
            questionImage.texture = loadedImages[questionCounter];
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

    void GameOver()
    {
        UI.SwitchPanels(gameOverPanel, gamePanel);
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

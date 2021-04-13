using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    public Text[] answerText = new Text[4];    
    public Text questionText;
    public Text scoreText;
    public Text finalScoreText;
    public Text recordText;
    public Text questionCounterText;
    public Text transitionScoreText;
    public GameObject gamePanel;
    public GameObject gameLoadingPanel;
    public GameObject transitionPanel;
    public GameObject gameOverPanel;
    public List<Texture> loadedImages = new List<Texture>();
    public RawImage questionImage;

    public float currentTime;
    public Text timerText;
    

    private void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        currentTime = 10.0f;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        timerText.text = currentTime.ToString("0");
        if (currentTime <= 0)
        {
            timerText.text = "0";
        }
    }
}

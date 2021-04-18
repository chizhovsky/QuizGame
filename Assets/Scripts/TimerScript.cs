using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private QuizManager quizManager;
    void Start()
    {
        quizManager = QuizManager.Instance;
        quizManager.currentTime = 10.0f;
    }

    void Update()
    {
        quizManager.currentTime -= 1 * Time.deltaTime;
        //UIManager.Instance.timerText.text = QuizManager.Instance.currentTime.ToString("0");
        if (quizManager.currentTime <= 0)
        {
           // UIManager.Instance.timerText.text = "0";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    void Update()
    {
        if (quizManager.countdownTimer.currentTime <= 0f)
        {
            quizManager.NextQuestion();
        }
    }

    public void Answer()
    {
        if (isCorrect)
        {
            quizManager.score = quizManager.CountPoints(quizManager.countdownTimer.currentTime);
            quizManager.scoreText.text = quizManager.score.ToString();
        }
        quizManager.NextQuestion();
    }
}

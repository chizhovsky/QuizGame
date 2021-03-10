using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect;
    public void Answer()
    {
        if (isCorrect)
        {
            QuizManager.instance.score = QuizManager.instance.CountPoints(UIManager.instance.currentTime);
            UIManager.instance.scoreText.text = QuizManager.instance.score.ToString();
        }
        QuizManager.instance.NextQuestion();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    private QuizManager quizManager;
    public bool isCorrect;
    private void Start() 
    {
        quizManager = QuizManager.Instance;        
    }
    public void Answer()
    {
        if (isCorrect)
        {
            quizManager.score = quizManager.CountPoints(quizManager.currentTime);
            //UIManager.Instance.scoreText.text = QuizManager.Instance.score.ToString();
            StartCoroutine(AnimationManager.Instance.RightAnimRoutine());
        }

        if (!isCorrect)
        {
            StartCoroutine(AnimationManager.Instance.WrongAnimRoutine());
        }
    }
}

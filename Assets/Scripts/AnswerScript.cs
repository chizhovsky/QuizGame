using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect;
    public Animator anim;
    public void AnimateRightAnswer()
    {
        anim.SetTrigger("Right");
    }
    public void AnimateWrongAnswer()
    {
        anim.SetTrigger("Wrong");
    }
    public void Answer()
    {
        if (isCorrect)
        {
            QuizManager.instance.score = QuizManager.instance.CountPoints(UIManager.instance.currentTime);
            UIManager.instance.scoreText.text = QuizManager.instance.score.ToString();
            StartCoroutine(RightAnswerRoutine());
        }
        if (!isCorrect)
        {
            StartCoroutine(WrongAnswerRoutine());
        }
    }
    IEnumerator RightAnswerRoutine()
    {
        AnimateRightAnswer();
        yield return new WaitForSeconds(1.5f);
        QuizManager.instance.NextQuestion();
        anim.Play("Button_Idle", 0, 0.0f);
    }

    IEnumerator WrongAnswerRoutine()
    {
        AnimateWrongAnswer();
        yield return new WaitForSeconds(1.5f);
        QuizManager.instance.NextQuestion();
        anim.Play("Button_Idle", 0, 0.0f);
    }
}

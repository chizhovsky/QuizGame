using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : Menu
{
    public Text questionText;
    public RawImage questionImage;
    public Text scoreText;
    [SerializeField] private Text _timerText;
    public Button[] answerButtons;
    public Text[] answerText;
    public Animator[] animators;
    public bool[] isCorrect;
    

    public void Init()
    {
        enabled = false;
        foreach (var btn in answerButtons)
        {
            btn.onClick.AddListener(delegate {ButtonPressed(btn); });
        }                
    }
    
    private void Update() 
    {
        if (GameplayManager.Instance.currentTime <= 0)
        {
            GameplayManager.Instance.NextQuestion();
        }

        GameplayManager.Instance.currentTime -= 1 * Time.deltaTime;
        _timerText.text = GameplayManager.Instance.currentTime.ToString("0");
        if (GameplayManager.Instance.currentTime <= 0)
        {
           _timerText.text = "0";
        }
    }

    private void ButtonPressed(Button btn)
    {
        foreach (var button in answerButtons)
        {
            button.interactable = false;
        }
        enabled = false;
        int i = int.Parse(btn.name)-1;
        int indexOfRight = 0;
        for (int j = 0; j < isCorrect.Length; j++)
        {
            if (isCorrect[j] == true)
            {
                indexOfRight = j;
            }            
        }
        if (i == indexOfRight)
        {
            GameplayManager.Instance.score = GameplayManager.Instance.CountPoints(GameplayManager.Instance.currentTime);
            scoreText.text = GameplayManager.Instance.score.ToString();
            StartCoroutine(PressRoutine(btn, true, i));
        }
        else
        {
            StartCoroutine(PressRoutine(btn, false, i));
        }
    }

    private IEnumerator PressRoutine(Button btn, bool isCorrect, int index)
    {
        if (isCorrect == true)
        {
            animators[index].SetTrigger("Right");
        }
        else
        {
            animators[index].SetTrigger("Wrong");
        }
        yield return new WaitForSeconds(3);
        animators[index].Play("Button_Idle");
        foreach (var button in answerButtons)
        {
            button.interactable = true;
        }
        enabled = true;
        GameplayManager.Instance.NextQuestion();
    }
}

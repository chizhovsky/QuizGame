using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager UI;
    private QuizManager Quiz;
    private void Start() 
    {
        UI = UIManager.Instance;
        Quiz = QuizManager.Instance;
        UI.Init();
        Quiz.Init();
    }
}

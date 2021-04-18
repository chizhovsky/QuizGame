using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoSingleton<AnimationManager>
{
    
    [HideInInspector] private Animator answersAnim;
    [HideInInspector] public Animator startPressedAnim;
    [HideInInspector] public Animator settingsPressedAnim;
    [HideInInspector] public Animator exitPressedAnim;
    public void StartButtonPressed()
    {
        StartCoroutine(StartPressRoutine());
    }

    public void SettingsButtonPressed()
    {
        StartCoroutine(SettingsPressRoutine());
    }

    public void ExitButtonPressed()
    {
        StartCoroutine(ExitPressRoutine());
    }
    

    IEnumerator StartPressRoutine()
    {
        startPressedAnim.SetTrigger("IsPressed");
        yield return new WaitForSeconds(1.5f);
        SceneManager.Instance.StartGame();
    }

    IEnumerator SettingsPressRoutine()
    {
        settingsPressedAnim.SetTrigger("IsPressed");
        yield return new WaitForSeconds(1.5f);
        //UIManager.Instance.SwitchPanels(QuizManager.Instance.settingsPanel, UIManager.Instance.menuPanel);
    }

    IEnumerator ExitPressRoutine()
    {
        exitPressedAnim.SetTrigger("IsPressed");
        yield return new WaitForSeconds(1.5f);
        SceneManager.Instance.QuitGame();
    }
    public IEnumerator RightAnimRoutine()
    {
        answersAnim.SetTrigger("Right");
        yield return new WaitForSeconds(1.5f);
        QuizManager.Instance.NextQuestion();
        if (QuizManager.Instance.questionCounter < 6)
        {
            answersAnim.Play("Button_Idle");
        }
    }
    public IEnumerator WrongAnimRoutine()
    {
        answersAnim.SetTrigger("Wrong");
        yield return new WaitForSeconds(1.5f);
        QuizManager.Instance.NextQuestion();
        if (QuizManager.Instance.questionCounter < 6)
        {
            answersAnim.Play("Button_Idle");
        }
    }
}

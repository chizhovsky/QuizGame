using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float currentTime;
    public Text timerText;
    public bool timeIsOver = false;

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
            timeIsOver = true;
        }
    }
}

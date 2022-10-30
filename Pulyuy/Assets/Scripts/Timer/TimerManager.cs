using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    float timer = 0f, startTime = 180f;
    [SerializeField] Canvas timerCanvas;
    [SerializeField] TextMeshProUGUI timerText;
    bool paused = false, win = false; 

    void Start()
    {
        timer = startTime;
    }

    void Update()
    {
        if(!paused)
        {
            timer -= Time.deltaTime;
            DisplayTime(timer);
        }
        if(timer <= 0 && !win)
        {
            PlayerMovement.instance.Win();
            win = true;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
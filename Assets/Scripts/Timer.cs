using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    float newGameTime = 0;
    TextMeshProUGUI timerText;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        float currentTime = Time.time;
        float deltaTime = currentTime - newGameTime;

        int minutes = (int)deltaTime / 60;
        int seconds = (int)deltaTime % 60;

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetNewGameTime()
    {
        newGameTime = Time.time;
    }

    public string GetGameTime()
    {
        float currentTime = Time.time;
        float deltaTime = currentTime - newGameTime;

        int minutes = (int)deltaTime / 60;
        int seconds = (int)deltaTime % 60;

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

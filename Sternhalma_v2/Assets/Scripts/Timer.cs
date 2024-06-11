using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 60; 
    private bool timeIsRunning = true;
    public TMP_Text timeText;
    public TMP_Text gameEndText; 

    void Start()
    {
    }

    void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeIsRunning = false;
                GameManager.Instance.ChangeState(GameState.LoseState);
            }
        }
        DisplayTime(timeRemaining);
        CheckGameStatus();
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(timeToDisplay, 0);
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void CheckGameStatus()
    {
        int piecesLeft  = GameObject.FindGameObjectsWithTag("Pieces").Length; 

        if (piecesLeft == 1) 
        {
            GameManager.Instance.ChangeState(GameState.WinState);
        }
    }

    public void DisplayEndGameText(string message)
    {
        gameEndText.text = message;
        gameEndText.gameObject.SetActive(true);
    }
}

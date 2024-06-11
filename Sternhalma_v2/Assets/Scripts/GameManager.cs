using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    private Timer _timer;

    private void Awake()
    {
        Instance = this;
        _timer = FindObjectOfType<Timer>();
    }

    private void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateHexGrid();
                break;
            case GameState.SpawnObjects:
                UnitManager.Instance.SpawnObjects();
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.WinState:
                HandleWinState();
                break;
            case GameState.LoseState:
                HandleLoseState();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void HandleWinState()
    {
        Debug.Log("Game Won!");
        _timer.DisplayEndGameText("You Win!");
    }

    private void HandleLoseState()
    {
        Debug.Log("Game Lost!");
        _timer.DisplayEndGameText("You Lose!");
    }
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnObjects = 1,
    PlayerTurn = 2,
    WinState = 3,
    LoseState = 4
}

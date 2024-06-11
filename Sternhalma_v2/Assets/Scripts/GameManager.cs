using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState GameState;
    private Timer _timer;

    private void Awake()
    {
        Debug.Log("Awake is called");
        Instance = this;
        _timer = FindObjectOfType<Timer>();
        //ChangeState(GameState.GenerateGrid);

    }

    private void Start()
    {
        //ChangeState(GameState.GenerateGrid);
        ChangeState(GameState.StartMenu);
        //GridManager.Instance.GenerateHexGrid();
        //Coroutine
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.StartMenu:
                //HandleStartMenu();
                break;
            case GameState.GenerateGrid:
                Debug.Log("GridManager Instance value is: ");
                Debug.Log(GridManager.Instance);
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

    private void HandleStartMenu()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    LoseState = 4,
    StartMenu = 5
}

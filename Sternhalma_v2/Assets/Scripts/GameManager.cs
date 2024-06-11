using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        Debug.Log($"Changing state from {GameState} to {newState}");

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
                Debug.Log("Player Wins!");
                break;
            case GameState.LoseState:
                Debug.Log("Player Loses!");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
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

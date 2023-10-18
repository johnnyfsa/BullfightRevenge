using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] InputManager inputManager;
    [SerializeField] UIOnPlayManager UIManager;

    [SerializeField] int score;

    [SerializeField] bool isPaused;

    public static GameManager Instance { get => _instance; private set => _instance = value; }

    public int Score { get => score; set => score = value; }

    private void Awake()
    {
        // Ensure that only one instance of the singleton exists.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        inputManager.OnPauseButtonPressed += ChangeGameState;
    }

    private void ChangeGameState(object sender, EventArgs e)
    {
        UIManager.ChangeUIGameState();
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    public void ChangeScore(int amount)
    {
        Score += amount;
        UIManager.UpdateScore(Score);
    }
}

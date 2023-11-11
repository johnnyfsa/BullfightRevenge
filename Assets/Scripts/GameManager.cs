using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum DiffcultyLevel { Easy, Medium, Hard };
    private static GameManager _instance;
    [SerializeField] InputManager inputManager;
    [SerializeField] UIManager uIManager;
    [SerializeField] int score;
    [SerializeField] DiffcultyLevel difficulty;
    [SerializeField] bool isPaused;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] bool isGameOver;
    [SerializeField] int scoreThreshold;

    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

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
        difficulty = DiffcultyLevel.Easy;
    }

    private void ChangeGameState(object sender, EventArgs e)
    {
        uIManager.ChangePauseState();
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
        inputManager.SwitchActiveActionMap();
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        inputManager.SwitchActiveActionMap();
    }

    public void ChangeScore(int amount)
    {
        Score += amount;
        //HUDManager.UpdateScore(Score);
        CheckScoreThreshold();
    }

    private void CheckScoreThreshold()
    {
        if (score >= scoreThreshold)
        {
            scoreThreshold *= 2;
            if (difficulty == DiffcultyLevel.Hard)
            {
                return;
            }
            ChangeDifficulty(difficulty + 1);
        }
    }

    private void ChangeDifficulty(DiffcultyLevel newDifficulty)
    {
        float spawnTimer = 5.0f;
        difficulty = newDifficulty;
        switch (difficulty)
        {
            case DiffcultyLevel.Easy:
                spawnTimer = 5.0f;
                break;
            case DiffcultyLevel.Medium:
                spawnTimer = 3.0f;
                break;
            case DiffcultyLevel.Hard:
                spawnTimer = 2.0f;
                break;
        }
        spawnManager.EnemySpawnTimer = spawnTimer;

    }

    public void GameOver()
    {
        isGameOver = true;
        //UIManager.GameOver();
        spawnManager.gameObject.SetActive(false);
        inputManager.OnPauseButtonPressed -= ChangeGameState;
        //Destroy(this.gameObject); 
    }
}

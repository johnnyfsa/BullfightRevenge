using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event Action<int> OnScoreChange;
    public event Action OnGameOver;
    public event Action<float> OnDifficultyChange;
    public event Action OnChangeGameState;
    enum DiffcultyLevel { Easy, Medium, Hard };
    private static GameManager _instance;
    [SerializeField] int score;
    [SerializeField] DiffcultyLevel difficulty;
    [SerializeField] bool isPaused;
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

    }

    void Start()
    {
        difficulty = DiffcultyLevel.Easy;
        InputManager.Instance.OnPauseButtonPressed += PauseButtonReaction;
    }

    private void PauseButtonReaction(object sender, EventArgs e)
    {
        ChangeGameState();
    }

    public void ChangeGameState()
    {
        if (isPaused)
        {
            ResumeGame();
            OnChangeGameState?.Invoke();
        }
        else
        {
            PauseGame();
            OnChangeGameState?.Invoke();
        }
        try
        {
            InputManager.Instance.SwitchActiveActionMap();
        }
        catch (NullReferenceException)
        {
            print("algo de errado não está certo");
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
        CheckScoreThreshold();
        OnScoreChange?.Invoke(Score);
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
        OnDifficultyChange?.Invoke(spawnTimer);

    }

    public void QuitGame()
    {
        //quit the editor
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            // O jogo está sendo executado como aplicativo build
            Debug.Log("Executando como Aplicativo");
        }
    }
    public void RestartGame()
    {
        ChangeGameState();
        if (isGameOver)
        {
            isGameOver = false;
            InputManager.Instance.OnPauseButtonPressed += PauseButtonReaction;
        }
        Score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        isGameOver = true;
        ChangeGameState();
        OnGameOver?.Invoke();
        InputManager.Instance.OnPauseButtonPressed -= PauseButtonReaction;
    }
}

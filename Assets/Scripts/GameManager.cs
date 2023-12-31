using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event Action<int> OnScoreChange;
    public event Action OnGameOver;
    public event Action<float> OnDifficultyChange;
    public event Action OnChangeGameState;
    public event Action<float> OnInsaneLevelReched;


    public event Action OnTopScoreChange;
    public enum DiffcultyLevel { Easy, Medium, Hard, Harder, Insane };
    private static GameManager _instance;
    [SerializeField] int score;
    [SerializeField] DiffcultyLevel difficulty;
    [SerializeField] bool isPaused;
    [SerializeField] bool isGameOver;
    [SerializeField] int scoreThreshold;

    private List<PlayerData> topScores = new List<PlayerData>();

    public bool isCoverScreen = false;

    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

    public static GameManager Instance { get => _instance; private set => _instance = value; }

    public int Score { get => score; set => score = value; }

    public DiffcultyLevel Difficulty { get => difficulty; set => difficulty = value; }

    public int ScoreThreshold { get => scoreThreshold; set => scoreThreshold = value; }

    private void Awake()
    {
        ScoreThreshold = 100;
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
        if (SceneManager.GetActiveScene().name == "Cover")
        {
            isCoverScreen = true;
        }
        difficulty = DiffcultyLevel.Easy;
        InputManager.Instance.OnPauseButtonPressed += PauseButtonReaction;
        topScores = SaveSystem.LoadTopScores();
    }

    private void PauseButtonReaction(object sender, EventArgs e)
    {
        ChangeGameState();
    }

    public void ChangeGameState()
    {
        if (!isCoverScreen)
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
                print("Active Action Map is Null");
            }
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
            if (difficulty == DiffcultyLevel.Insane)
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
            case DiffcultyLevel.Harder:
                spawnTimer = 1.0f;
                break;
            case DiffcultyLevel.Insane:
                spawnTimer = 0.5f;
                OnInsaneLevelReched?.Invoke(0.5f);
                break;
        }
        OnDifficultyChange?.Invoke(spawnTimer);

    }

    public void QuitGame()
    {
        //quit the editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        ReturnToTitle();
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
        InputManager.Instance.OnPauseButtonPressed -= PauseButtonReaction;
        OnGameOver?.Invoke();
        if (IsNewHighScore())
        {
            OnTopScoreChange?.Invoke();
        }
    }
    private bool IsNewHighScore()
    {
        if (topScores.Count == 0)
        {
            return true;
        }
        else
        {
            foreach (PlayerData playerData in topScores)
            {
                if (score > playerData.Score)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void ReturnToTitle()
    {
        ChangeGameState();
        isCoverScreen = true;
        score = 0;
        SceneManager.LoadScene("Cover");
    }

    public void SaveTopScore(string playerName)
    {
        topScores.Add(new PlayerData(playerName, score));
        topScores.Sort((x, y) => y.Score.CompareTo(x.Score));
        if (topScores.Count > 5)
        {
            topScores.RemoveAt(5);
        }
        SaveSystem.SaveTopScores(topScores);
    }

    public List<PlayerData> GetTopScores()
    {
        return topScores;
    }
}

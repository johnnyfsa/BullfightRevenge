using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] InputManager inputManager;
    [SerializeField] UIOnPlayManager UIManager;

    [SerializeField] bool isPaused;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Ensure that only one instance of the singleton exists.
        if (_instance == null)
        {
            _instance = this;
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
}

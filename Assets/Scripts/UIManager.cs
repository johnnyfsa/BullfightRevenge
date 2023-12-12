using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] UIUnit[] Screens;
    private UIUnit pauseMenu, optionsScreen, addNewHighScoreScreen, highScoresScreen, gameOverScreen;
    // private static UIManager instance;
    //public static UIManager Instance { get => instance; set => instance = value; }
    private void OnEnable()
    {
        GameManager.Instance.OnChangeGameState += TogglePauseState;
        GameManager.Instance.OnTopScoreChange += ShowAddNewHighScore;
        pauseMenu = Array.Find(Screens, screen => screen.type == UIType.PauseMenu);
        PauseMenuController pauseMenuController = pauseMenu.uiDocument.GetComponent<PauseMenuController>();
        pauseMenuController.OnOpenOptions += OpenOptions;
        optionsScreen = Array.Find(Screens, screen => screen.type == UIType.Options);
        OptionMenuController optionMenuController = optionsScreen.uiDocument.GetComponent<OptionMenuController>();
        optionMenuController.OnCloseOptions += CloseOptions;
        GameManager.Instance.OnGameOver += GameOver;
        gameOverScreen = Array.Find(Screens, screen => screen.type == UIType.GameOver);
        GameOverScreenController gameOverController = gameOverScreen.uiDocument.GetComponent<GameOverScreenController>();
        gameOverController.OnHighScoreSelected += ShowHighScoreScreen;
        addNewHighScoreScreen = Array.Find(Screens, screen => screen.type == UIType.AddHighScore);
        AddNewHighScore addNewHighScoreScreenController = addNewHighScoreScreen.uiDocument.GetComponent<AddNewHighScore>();
        addNewHighScoreScreenController.OnNewHighScoreSubmit += CloseAddNewHighScores;
        highScoresScreen = Array.Find(Screens, screen => screen.type == UIType.HighScores);
        HighScoreUIController hscoreScreenController = highScoresScreen.uiDocument.GetComponent<HighScoreUIController>();
        hscoreScreenController.OnScreenClosed += CloseHighScores;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnChangeGameState -= TogglePauseState;
        GameManager.Instance.OnTopScoreChange -= ShowAddNewHighScore;
        PauseMenuController pauseMenuController = pauseMenu.uiDocument.GetComponent<PauseMenuController>();
        pauseMenuController.OnOpenOptions -= OpenOptions;
        OptionMenuController optionMenuController = optionsScreen.uiDocument.GetComponent<OptionMenuController>();
        optionMenuController.OnCloseOptions -= CloseOptions;
        AddNewHighScore addNewHighScoreScreenController = addNewHighScoreScreen.uiDocument.GetComponent<AddNewHighScore>();
        addNewHighScoreScreenController.OnNewHighScoreSubmit -= CloseAddNewHighScores;
        GameManager.Instance.OnGameOver -= GameOver;
        GameOverScreenController gameOverController = gameOverScreen.uiDocument.GetComponent<GameOverScreenController>();
        gameOverController.OnHighScoreSelected -= ShowHighScoreScreen;
        HighScoreUIController hscoreScreenController = highScoresScreen.uiDocument.GetComponent<HighScoreUIController>();
        hscoreScreenController.OnScreenClosed -= CloseHighScores;

    }

    private void CloseHighScores()
    {
        if (gameOverScreen.uiDocument.gameObject.activeInHierarchy == false)
        {
            gameOverScreen.uiDocument.gameObject.SetActive(true);
        }
        highScoresScreen.uiDocument.gameObject.SetActive(false);
    }

    private void ShowHighScoreScreen()
    {
        if (gameOverScreen.uiDocument.gameObject.activeInHierarchy == true)
        {
            gameOverScreen.uiDocument.gameObject.SetActive(false);
        }
        highScoresScreen.uiDocument.gameObject.SetActive(true);
    }

    private void CloseAddNewHighScores()
    {
        if (gameOverScreen.uiDocument.gameObject.activeInHierarchy == false)
        {
            gameOverScreen.uiDocument.gameObject.SetActive(true);
        }
        addNewHighScoreScreen.uiDocument.gameObject.SetActive(false);
    }

    public void TogglePauseState()
    {
        if (!GameManager.Instance.IsGameOver)
        {
            if (optionsScreen.uiDocument.gameObject.activeInHierarchy)
            {
                CloseOptions();
            }
            bool activeState = pauseMenu.uiDocument.gameObject.activeInHierarchy;
            pauseMenu.uiDocument.gameObject.SetActive(!activeState);
        }

    }


    public void OpenOptions()
    {

        pauseMenu.uiDocument.gameObject.SetActive(false);
        optionsScreen.uiDocument.gameObject.SetActive(true);
    }


    public void CloseOptions()
    {
        pauseMenu.uiDocument.gameObject.SetActive(true);
        optionsScreen.uiDocument.gameObject.SetActive(false);
    }
    public UIOnPlayManager GetActiveUI(UIType type)
    {
        UIUnit screen = Array.Find(Screens, screen => screen.type == type);
        UIOnPlayManager uiManager = screen.uiDocument.GetComponent<UIOnPlayManager>();
        if (uiManager != null)
        {
            return uiManager;
        }
        return null;
    }

    public void GameOver()
    {
        gameOverScreen.uiDocument.gameObject.SetActive(true);
    }

    public void ShowAddNewHighScore()
    {
        if (gameOverScreen.uiDocument.gameObject.activeInHierarchy == true)
        {
            gameOverScreen.uiDocument.gameObject.SetActive(false);
        }
        addNewHighScoreScreen.uiDocument.gameObject.SetActive(true);
    }

}

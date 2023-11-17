using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] UIUnit[] Screens;
    // private static UIManager instance;
    //public static UIManager Instance { get => instance; set => instance = value; }
    private void OnEnable()
    {
        GameManager.Instance.OnChangeGameState += TogglePauseState;
        UIUnit pauseMenu = Array.Find(Screens, screen => screen.type == UIType.PauseMenu);
        PauseMenuController pauseMenuController = pauseMenu.uiDocument.GetComponent<PauseMenuController>();
        pauseMenuController.OnOpenOptions += OpenOptions;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnChangeGameState -= TogglePauseState;
    }

    public void TogglePauseState()
    {
        UIUnit optionsScreen = Array.Find(Screens, screen => screen.type == UIType.Options);
        if (optionsScreen.uiDocument.gameObject.activeInHierarchy)
        {
            CloseOptions();
        }
        UIUnit pauseScreen = Array.Find(Screens, screen => screen.type == UIType.PauseMenu);
        bool activeState = pauseScreen.uiDocument.gameObject.activeInHierarchy;
        pauseScreen.uiDocument.gameObject.SetActive(!activeState);
    }


    public void OpenOptions()
    {
        UIUnit optionsScreen = Array.Find(Screens, screen => screen.type == UIType.Options);
        UIUnit pauseScreen = Array.Find(Screens, screen => screen.type == UIType.PauseMenu);
        pauseScreen.uiDocument.gameObject.SetActive(false);
        optionsScreen.uiDocument.gameObject.SetActive(true);
    }


    public void CloseOptions()
    {
        UIUnit optionsScreen = Array.Find(Screens, screen => screen.type == UIType.Options);
        UIUnit pauseScreen = Array.Find(Screens, screen => screen.type == UIType.PauseMenu);
        pauseScreen.uiDocument.gameObject.SetActive(true);
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

}

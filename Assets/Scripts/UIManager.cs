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
    }

    private void OnDisable()
    {
        GameManager.Instance.OnChangeGameState -= TogglePauseState;
    }

    public void TogglePauseState()
    {
        UIUnit pauseScreen = Array.Find(Screens, screen => screen.type == UIType.PauseMenu);
        bool activeState = pauseScreen.uiDocument.gameObject.activeInHierarchy;
        pauseScreen.uiDocument.gameObject.SetActive(!activeState);
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

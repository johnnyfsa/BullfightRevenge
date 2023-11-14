using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] UIUnit[] Screens;

    public void ChangePauseState()
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

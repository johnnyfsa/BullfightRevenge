using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoverUIController : MonoBehaviour
{
    [SerializeField] CoverBull bull;
    [SerializeField] UIDocument coverScreen;
    [SerializeField] UIDocument highScoresScreen;
    private StartMenuController startMenuController;
    private HighScoreUIController highScoreUIController;
    void Awake()
    {
        bull.OnBullDestroyed += Init;
        startMenuController = coverScreen.gameObject.GetComponent<StartMenuController>();
        startMenuController.OnHighScoreSelected += DisplayHighScoresScreen;
        highScoreUIController = highScoresScreen.gameObject.GetComponent<HighScoreUIController>();
        highScoreUIController.OnScreenClosed += DisplayCoverScreen;
    }

    private void DisplayCoverScreen()
    {
        DeactivateAllScreens();
        coverScreen.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void DisplayHighScoresScreen()
    {
        DeactivateAllScreens();
        highScoresScreen.rootVisualElement.style.display = DisplayStyle.Flex;
    }
    private void DeactivateAllScreens()
    {
        coverScreen.rootVisualElement.style.display = DisplayStyle.None;
        highScoresScreen.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void Init()
    {
        //enable all screens
        coverScreen.gameObject.SetActive(true);
        highScoresScreen.gameObject.SetActive(true);
        DisplayCoverScreen();
        bull.OnBullDestroyed -= Init;
    }
}

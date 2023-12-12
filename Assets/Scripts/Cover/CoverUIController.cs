using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoverUIController : MonoBehaviour
{
    [SerializeField] CoverBull bull;
    [SerializeField] List<UIUnit> Screens;
    private StartMenuController startMenuController;
    private HighScoreUIController highScoreUIController;
    private OptionMenuController optionMenuController;

    void Awake()
    {
        bull.OnBullDestroyed += Init;
    }
    private void Start()
    {
        var cover = Screens.Find(x => x.type == UIType.Cover);
        startMenuController = cover.uiDocument.GetComponent<StartMenuController>();
        var hScores = Screens.Find(x => x.type == UIType.HighScores);
        highScoreUIController = hScores.uiDocument.GetComponent<HighScoreUIController>();
        var options = Screens.Find(x => x.type == UIType.Options);
        optionMenuController = options.uiDocument.GetComponent<OptionMenuController>();
        startMenuController.OnHighScoreSelected += ShowHighScores;
        startMenuController.OnOptionsSelected += ShowOptions;
        highScoreUIController.OnScreenClosed += ShowCoverMenu;
        optionMenuController.OnCloseOptions += ShowCoverMenu;
    }

    private void ShowOptions()
    {
        DesactivateAllScreensExcept(UIType.Options);
        optionMenuController.gameObject.SetActive(true);
    }

    private void ShowCoverMenu()
    {
        DesactivateAllScreensExcept(UIType.Cover);
        startMenuController.gameObject.SetActive(true);
    }

    private void ShowHighScores()
    {
        DesactivateAllScreensExcept(UIType.HighScores);
        highScoreUIController.gameObject.SetActive(true);
    }

    private void DesactivateAllScreensExcept(UIType screenToKeep)
    {
        foreach (UIUnit screen in Screens)
        {
            if (screen.type != screenToKeep)
            {
                screen.uiDocument.gameObject.SetActive(false);
            }
        }
    }

    private void Init()
    {
        startMenuController.FirstLoad = true;
        startMenuController.gameObject.SetActive(true);
        bull.OnBullDestroyed -= Init;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverScreenController : MonoBehaviour
{
    public event Action OnHighScoreSelected;
    VisualElement root;
    Button restartBtn, highScoresBtn, quitBtn;
    private List<Button> buttons;
    private int index;
    void OnEnable()
    {
        index = 0;
        root = GetComponent<UIDocument>().rootVisualElement;
        restartBtn = root.Q<Button>("restartBtn");
        highScoresBtn = root.Q<Button>("highScoresBtn");
        quitBtn = root.Q<Button>("quitBtn");
        buttons = new List<Button>() { restartBtn, highScoresBtn, quitBtn };
        restartBtn.Focus();


        restartBtn.RegisterCallback<ClickEvent>(RestartOnClick);
        highScoresBtn.RegisterCallback<ClickEvent>(OpenHighScoresOnClick);
        quitBtn.RegisterCallback<ClickEvent>(ExitOnClick);
        root.RegisterCallback<KeyDownEvent>(ConfirmAction);
        root.RegisterCallback<KeyDownEvent>(OnNavigateUI, TrickleDown.TrickleDown);
    }

    private void ExitOnClick(ClickEvent evt)
    {
        GameManager.Instance.QuitGame();
    }

    private void OpenHighScoresOnClick(ClickEvent evt)
    {
        OnHighScoreSelected?.Invoke();
    }

    private void OnNavigateUI(KeyDownEvent evt)
    {
        KeyCode keyPressed = evt.keyCode;
        switch (keyPressed)
        {
            case KeyCode.UpArrow:
                index--;
                if (index < 0)
                {
                    index = buttons.Count - 1;
                }
                break;
            case KeyCode.DownArrow:
                index++;
                if (index > buttons.Count - 1)
                {
                    index = 0;
                }
                break;
            case KeyCode.W:
                index--;
                if (index < 0)
                {
                    index = buttons.Count - 1;
                }
                break;
            case KeyCode.S:
                index++;
                if (index > buttons.Count - 1)
                {
                    index = 0;
                }
                break;
        }
        buttons[index].Focus();
    }

    private void ConfirmAction(KeyDownEvent evt)
    {
        if (evt.keyCode == KeyCode.Space || evt.keyCode == KeyCode.Return)
        {
            Button button = buttons.Find(x => x == x.panel.focusController.focusedElement);
            switch (button.name)
            {
                case "restartBtn":
                    GameManager.Instance.RestartGame();
                    break;
                case "highScoresBtn":
                    OnHighScoreSelected?.Invoke();
                    break;
                case "quitBtn":
                    GameManager.Instance.QuitGame();
                    break;
            }

        }
    }

    private void RestartOnClick(ClickEvent evt)
    {
        GameManager.Instance.RestartGame();
    }
}

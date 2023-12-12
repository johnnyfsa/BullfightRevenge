using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuController : MonoBehaviour
{
    public event Action OnOpenOptions;
    private VisualElement root;
    private List<Button> buttons;

    private Button resumeBtn, optionsBtn, restartBtn, quitBtn;


    private int index;

    // Start is called before the first frame update
    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        Button resume = root.Q<Button>("resume");
        resume.Focus();
        index = 0;
        buttons = root.Query<Button>().ToList();
        resumeBtn = root.Q<Button>("resume");
        optionsBtn = root.Q<Button>("options");
        restartBtn = root.Q<Button>("restart");
        quitBtn = root.Q<Button>("quit");
        root.RegisterCallback<KeyDownEvent>(OnNavigateUI, TrickleDown.TrickleDown);
        root.RegisterCallback<KeyDownEvent>(ConfirmAction, TrickleDown.TrickleDown);
        resumeBtn.RegisterCallback<ClickEvent>(OnResumeButonClicked);
        optionsBtn.RegisterCallback<ClickEvent>(OnOptionsButonClicked);
        restartBtn.RegisterCallback<ClickEvent>(OnRestartButonClicked);
        quitBtn.RegisterCallback<ClickEvent>(OnQuitButonClicked);
    }

    private void OnQuitButonClicked(ClickEvent evt)
    {
        GameManager.Instance.QuitGame();
    }

    private void OnRestartButonClicked(ClickEvent evt)
    {
        GameManager.Instance.RestartGame();
    }

    private void OnOptionsButonClicked(ClickEvent evt)
    {
        OpenOptions();
    }

    private void OpenOptions()
    {
        OnOpenOptions?.Invoke();
    }

    private void OnResumeButonClicked(ClickEvent evt)
    {
        GameManager.Instance.ChangeGameState();
    }

    protected void ConfirmAction(KeyDownEvent evt)
    {
        if (evt.keyCode == KeyCode.Space || evt.keyCode == KeyCode.Return)
        {
            Button button = buttons.Find(x => x == x.panel.focusController.focusedElement);
            switch (button.name)
            {
                case "resume":
                    GameManager.Instance.ChangeGameState();
                    break;
                case "quit":
                    GameManager.Instance.ReturnToTitle();
                    break;
                case "restart":
                    GameManager.Instance.RestartGame();
                    break;
                case "options":
                    OpenOptions();
                    break;
            }
        }
    }

    protected void OnNavigateUI(KeyDownEvent evt)
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

}

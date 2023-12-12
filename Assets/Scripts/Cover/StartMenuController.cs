using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartMenuController : MonoBehaviour
{
    public event Action OnHighScoreSelected;
    public event Action OnOptionsSelected;
    VisualElement root, title, btnContainer;

    private bool firstLoad;
    List<Button> buttons;

    Button startBtn, optionsBtn, hscoresButton;
    int index;

    public bool FirstLoad { get => firstLoad; set => firstLoad = value; }

    // Start is called before the first frame update
    void OnEnable()
    {
        index = 0;
        root = GetComponent<UIDocument>().rootVisualElement;
        title = root.Q<VisualElement>("title");
        btnContainer = root.Q<VisualElement>("btnContainer");
        buttons = btnContainer.Query<Button>().ToList();
        startBtn = buttons[0];
        hscoresButton = buttons[1];
        optionsBtn = buttons[2];

        root.RegisterCallback<KeyDownEvent>(ConfirmAction, TrickleDown.TrickleDown);
        root.RegisterCallback<KeyDownEvent>(OnNavigateUI, TrickleDown.TrickleDown);
        startBtn.RegisterCallback<ClickEvent>(evt => { SceneManager.LoadScene("Main"); });
        hscoresButton.RegisterCallback<ClickEvent>(evt => { OnHighScoreSelected?.Invoke(); });
        if (FirstLoad)
        {
            title.RemoveFromClassList("titleEnd");
            btnContainer.RemoveFromClassList("btnContainer--top");
            StartCoroutine(ShowMenu());
        }
        startBtn.Focus();
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
        if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.Space)
        {
            Button button = buttons.Find(x => x == x.panel.focusController.focusedElement);
            switch (button.name)
            {
                case "startBtn":
                    GameManager.Instance.isCoverScreen = false;
                    SceneManager.LoadScene("Main");
                    break;
                case "highScoresBtn":
                    OnHighScoreSelected?.Invoke();
                    break;
                case "optionsBtn":
                    OnOptionsSelected?.Invoke();
                    break;
            }
        }
    }

    IEnumerator ShowMenu()
    {
        yield return new WaitForSeconds(0.5f);
        title.AddToClassList("titleEnd");
        btnContainer.AddToClassList("btnContainer--top");
        firstLoad = false;
    }
    // Update is called once per frame
    void Update()
    {
    }
}

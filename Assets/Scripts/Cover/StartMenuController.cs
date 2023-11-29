using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartMenuController : MonoBehaviour
{
    VisualElement root, title, btnContainer;
    List<Button> buttons;

    Button startBtn, optionsBtn, hscoresButton;
    int index;
    // Start is called before the first frame update
    void OnEnable()
    {
        index = 0;
        root = GetComponent<UIDocument>().rootVisualElement;
        title = root.Q<VisualElement>("title");
        btnContainer = root.Q<VisualElement>("btnContainer");
        buttons = btnContainer.Query<Button>().ToList();
        buttons[index].Focus();
        startBtn = buttons[0];
        optionsBtn = buttons[1];
        hscoresButton = buttons[2];

        root.RegisterCallback<KeyDownEvent>(StartGame);
        StartCoroutine(ShowMenu());
    }

    private void StartGame(KeyDownEvent evt)
    {
        if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.Space)
        {
            Button button = buttons.Find(x => x == x.panel.focusController.focusedElement);
            switch (button.name)
            {
                case "startBtn":
                    SceneManager.LoadScene("Main");
                    break;
                case "highScoreBtn":
                    break;
                case "optionsBtn":
                    break;
            }
        }
    }

    IEnumerator ShowMenu()
    {
        yield return new WaitForSeconds(0.5f);
        title.AddToClassList("titleEnd");
        btnContainer.AddToClassList("btnContainer--top");
    }
    // Update is called once per frame
    void Update()
    {
    }
}

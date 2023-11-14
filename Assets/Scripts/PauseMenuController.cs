using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuController : MonoBehaviour
{
    private VisualElement root;
    private List<Button> buttons;

    private int index;

    // Start is called before the first frame update
    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        Button resume = root.Q<Button>("resume");
        resume.Focus();
        index = 0;
        buttons = root.Query<Button>().ToList();
        root.RegisterCallback<KeyDownEvent>(OnNavigateUI, TrickleDown.TrickleDown);
        root.RegisterCallback<KeyDownEvent>(ConfirmAction, TrickleDown.TrickleDown);
    }

    private void ConfirmAction(KeyDownEvent evt)
    {
        if (evt.keyCode == KeyCode.Space || evt.keyCode == KeyCode.Return)
        {
            Button button = buttons.Find(x => x == x.panel.focusController.focusedElement);
            switch (button.name)
            {
                case "resume":
                    GameManager.Instance.ChangeGameState();
                    break;
            }
        }
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
}

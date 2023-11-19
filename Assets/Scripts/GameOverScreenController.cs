using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverScreenController : MonoBehaviour
{
    VisualElement root;
    Button restartBtn;
    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        restartBtn = root.Q<Button>("restartBtn");
        restartBtn.Focus();

        restartBtn.RegisterCallback<ClickEvent>(RestartOnClick);
        restartBtn.RegisterCallback<KeyDownEvent>(ConfirmAction);
    }

    private void ConfirmAction(KeyDownEvent evt)
    {
        if (evt.keyCode == KeyCode.Space || evt.keyCode == KeyCode.Return)
        {
            GameManager.Instance.RestartGame();
        }
    }

    private void RestartOnClick(ClickEvent evt)
    {
        GameManager.Instance.RestartGame();
    }
}

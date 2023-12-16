using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AddNewHighScore : MonoBehaviour
{
    public event Action OnNewHighScoreSubmit;

    VisualElement root;
    TextField playerName;
    Button submitButton;
    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        submitButton = root.Q<Button>("submit");
        playerName = root.Q<TextField>("playerName");
        submitButton.RegisterCallback<ClickEvent>(OnSubmitClicked);
        root.RegisterCallback<KeyDownEvent>(CofirmName);
    }

    private void CofirmName(KeyDownEvent evt)
    {
        KeyCode keyPressed = evt.keyCode;
        switch (keyPressed)
        {
            case KeyCode.Return:
                HandleHighScoreName();
                break;
        }
    }

    private void OnSubmitClicked(ClickEvent evt)
    {
        HandleHighScoreName();
    }

    private void HandleHighScoreName()
    {
        string text = playerName.text;
        if (text.Length > 9)
        {
            text = text.Substring(0, 19);
        }
        if (text != null && text.Length > 0)
        {
            OnNewHighScoreSubmit?.Invoke();
            GameManager.Instance.SaveTopScore(text);
        }
    }

    void OnDisable()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using System;

public class UIOnPlayManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] InputManager inputManager;
    private Label numLives;
    private Label score;

    private void Awake()
    {
        player.OnLivesChanged += UpdateLives;
    }

    public void ChangeUIGameState()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement pauseMenu = root.Q<VisualElement>("PauseMenu");
        SwitchDisplayState(pauseMenu);
        print("Pause Menu Displayed");
    }

    private void SwitchDisplayState(VisualElement display)
    {
        if (display.style.display != DisplayStyle.Flex)
        {
            display.style.display = DisplayStyle.Flex;
        }
        else
        {
            display.style.display = DisplayStyle.None;
        }

    }

    private void UpdateLives(object sender, EventArgs e)
    {
        numLives.text = "Num Lives: " + player.NumLives;
    }

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        score = root.Q<Label>("Score");
        numLives = root.Q<Label>("NumLives");
        numLives.text = "Num Lives:" + player.NumLives;
        score.text = "Score: 0";
    }

    internal void UpdateScore(int score)
    {
        this.score.text = "Score: " + score;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using System;

public class UIOnPlayManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] InputManager inputManager;
    [SerializeField] Texture2D velocityPowerUpIcon;
    [SerializeField] Texture2D stompPowerUpIcon;
    private Label numLives;
    private Label score;
    private VisualElement vPowerUpIcon;
    private VisualElement sPowerUpIcon;

    private void Awake()
    {
        player.OnLivesChanged += UpdateLives;
        player.OnPowerUpChanged += UpdatePowerUp;
    }

    private void UpdatePowerUp(object sender, Player.PowerUpArgs e)
    {
        if (e._speedMultiplier >= 1.5f)
        {
            vPowerUpIcon.style.visibility = Visibility.Visible;

        }
        else if (e._speedMultiplier < 1.5f)
        {
            vPowerUpIcon.style.visibility = Visibility.Hidden;
        }
        if (e._isStompActive)
        {
            sPowerUpIcon.style.visibility = Visibility.Visible;
        }
        else
        {
            sPowerUpIcon.style.visibility = Visibility.Hidden;

        }
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
        vPowerUpIcon = root.Q<VisualElement>("VelocityPowerUpIcon");
        vPowerUpIcon.style.backgroundImage = new StyleBackground(velocityPowerUpIcon);
        vPowerUpIcon.style.display = DisplayStyle.Flex;
        vPowerUpIcon.style.visibility = Visibility.Hidden;
        sPowerUpIcon = root.Q<VisualElement>("StompPowerUpIcon");
        sPowerUpIcon.style.backgroundImage = new StyleBackground(stompPowerUpIcon);
        sPowerUpIcon.style.display = DisplayStyle.Flex;
        sPowerUpIcon.style.visibility = Visibility.Hidden;
    }

    internal void UpdateScore(int score)
    {
        this.score.text = "Score: " + score;
    }
}

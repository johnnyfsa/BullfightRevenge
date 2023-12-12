using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using System;
using System.Threading;

public class UIOnPlayManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Texture2D bullHead;
    [SerializeField] Texture2D stompIcon;
    private Label score;
    private VisualElement vPowerUpIcon;

    private List<VisualElement> lifeIcons;
    private List<VisualElement> stompIcons;

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
        HiedAllStompIcons();
        for (int i = 0; i < e._numStomps; i++)
        {
            stompIcons[i].style.visibility = Visibility.Visible;
        }
    }

    private void UpdateLives(object sender, EventArgs e)
    {
        HideAllLifeIcons();
        for (int i = 0; i < player.NumLives; i++)
        {
            lifeIcons[i].style.visibility = Visibility.Visible;
        }
    }

    private void OnEnable()
    {
        lifeIcons = new List<VisualElement>();
        GameManager.Instance.OnScoreChange += UpdateScore;
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        score = root.Q<Label>("Score");
        var numLivesPanel = root.Q<VisualElement>("NumLivesPanel");
        lifeIcons = numLivesPanel.Query<VisualElement>("BullHead").ToList();
        HideAllLifeIcons();
        for (int i = 0; i < player.NumLives; i++)
        {
            lifeIcons[i].style.visibility = Visibility.Visible;
        }
        score.text = "Score: 0";
        vPowerUpIcon = root.Q<VisualElement>("VelocityPowerUpIcon");
        vPowerUpIcon.style.display = DisplayStyle.Flex;
        vPowerUpIcon.style.visibility = Visibility.Hidden;
        stompIcons = root.Query<VisualElement>("StompPowerUpIcon").ToList();
        HiedAllStompIcons();

    }

    internal void UpdateScore(int score)
    {
        this.score.text = "Score: " + score;
    }

    private void HideAllLifeIcons()
    {
        foreach (VisualElement lifeIcon in lifeIcons)
        {
            lifeIcon.style.visibility = Visibility.Hidden;
        }
    }

    private void HiedAllStompIcons()
    {
        foreach (VisualElement stompIcon in stompIcons)
        {
            stompIcon.style.visibility = Visibility.Hidden;
        }
    }
}

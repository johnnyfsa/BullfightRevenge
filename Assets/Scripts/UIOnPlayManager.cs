using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using System;

public class UIOnPlayManager : MonoBehaviour
{
    [SerializeField] Player player;
    private Label numLives;
    private void Awake()
    {
        player.OnLivesChanged += UpdateLives;
    }

    private void UpdateLives(object sender, EventArgs e)
    {
        numLives.text = "Num Lives: " + player.NumLives;
    }

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        numLives = root.Q<Label>("NumLives");
        numLives.text = "Num Lives: 3";
    }
}

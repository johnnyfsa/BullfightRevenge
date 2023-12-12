using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string PlayerName;
    public int Score;

    public PlayerData(string name, int score)
    {
        this.PlayerName = name;
        this.Score = score;
    }
}

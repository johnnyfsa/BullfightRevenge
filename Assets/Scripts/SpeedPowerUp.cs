using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpeedPowerUp : PowerUp
{
    [SerializeField] float _speedMultiplier = 1.5f;
    public override void Activate(Player player)
    {
        player.SpeedMultiplyer *= _speedMultiplier;
    }

}

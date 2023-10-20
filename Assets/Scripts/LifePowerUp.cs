using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePowerUp : PowerUp
{
    public override void Activate(Player player)
    {
        player.AddLives(1);
    }
}

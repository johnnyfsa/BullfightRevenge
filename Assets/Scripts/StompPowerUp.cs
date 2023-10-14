using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompPowerUp : PowerUp
{
    public override void Activate(Player player)
    {
        player.StompActive = true;
    }
}

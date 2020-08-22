using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : Enemy
{
    // kills the player through health script
    protected override void PlayerImpact(Player player)
    {
        Health playerHealth = player.GetComponent<Health>();
        playerHealth.Kill();
    }
}

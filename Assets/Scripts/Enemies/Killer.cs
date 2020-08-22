using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : Enemy
{

    protected override void PlayerImpact(Player player)
    {
        Health playerHealth = player.GetComponent<Health>();
        playerHealth.Kill();
    }
}

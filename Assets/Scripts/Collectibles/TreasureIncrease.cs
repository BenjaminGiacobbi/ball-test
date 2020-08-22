using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureIncrease : CollectibleBase
{
    [SerializeField] int _treasureValue = 1;

    protected override void Collect(Player player)
    {
        player.CollectTreasure(_treasureValue);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncrease : CollectibleBase
{
    // adjustible for expansion purposes
    [SerializeField] int _healthAdded = 1;

    // on collect, heals player through health script
    protected override void Collect(Player player)
    {
        player.GetComponent<Health>()?.IncreaseHealth(_healthAdded);
    }
}

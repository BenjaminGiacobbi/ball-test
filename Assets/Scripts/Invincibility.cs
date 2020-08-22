using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
    [SerializeField] Material _powerUpMaterial = null;
    private Material tempMaterial = null;

    protected override void PowerUp(Player player)
    {
        // sets invincible and changes player visual
        player.GetComponent<Health>()?.SetInvincible(true);
        MeshRenderer playerRenderer = player.GetComponent<MeshRenderer>();

        // stores current material to reassign later
        tempMaterial = playerRenderer.material;
        playerRenderer.material = _powerUpMaterial;
    }

    protected override void PowerDown(Player player)
    {
        player.GetComponent<Health>()?.SetInvincible(false);

        // resets original material and nullifies temp material
        MeshRenderer playerRenderer = player.GetComponent<MeshRenderer>();
        playerRenderer.material = tempMaterial;
        tempMaterial = null;

        gameObject.SetActive(false);
    }
}

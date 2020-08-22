using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
    [SerializeField] Material _powerUpMaterial = null;
    [SerializeField] AudioClip _invincibilityJingle = null;
    private Material _tempMaterial = null;

    // player reference to continue to affect player after initial contact
    private Player _playerReference = null;


    // sets powerup on player and keeps references to power down later
    protected override void PowerUp(Player player)
    {
        // sets player reference to affect invincibility state for power-up duration
        _playerReference = player;
        _playerReference.GetComponent<Health>()?.SetInvincible(true);

        AudioHelper.PlayClip2D(_invincibilityJingle, 0.3f, _powerUpDuration);
        

        // stores current material to reassign later
        MeshRenderer playerRenderer = _playerReference.GetComponent<MeshRenderer>();
        _tempMaterial = playerRenderer.material;
        playerRenderer.material = _powerUpMaterial;
    }


    // reverts powerup
    protected override void PowerDown()
    {
        _playerReference.GetComponent<Health>()?.SetInvincible(false);

        // resets original material and nullifies temp material
        MeshRenderer playerRenderer = _playerReference.GetComponent<MeshRenderer>();
        playerRenderer.material = _tempMaterial;
        _tempMaterial = null;


        gameObject.SetActive(false);
    }
}

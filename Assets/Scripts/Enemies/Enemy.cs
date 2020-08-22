using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] int _damageAmount = 1;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound;

    Rigidbody _rb;

    // caching
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    // move is in fixedUpdate to account for framerate
    private void FixedUpdate()
    {
        Move();
    }


    // on collision, determines impact if player script is detected
    private void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null)
        {
            PlayerImpact(player);
            ImpactFeedback(other.GetContact(0).point);
        }
    }


    // accesses health script on player
    protected virtual void PlayerImpact(Player player)
    {
        Health playerHealth = player.GetComponent<Health>();
        playerHealth?.DecreaseHealth(_damageAmount);
   
    }


    // plays particle feedback, universal impact
    // TODO add functionality to have different impacts based on player state -- unsure the best way to communicate this, had trouble implementing it here
    private void ImpactFeedback(Vector3 impactPosition)
    {
        // particles
        if(_impactParticles != null)
        {
            _impactParticles = Instantiate
                (_impactParticles, impactPosition, Quaternion.identity);
        }

        // sound, consider object pooling for performance 
        if(_impactSound != null)
        {
            AudioHelper.PlayClip2D(_impactSound, 1f);
        }
    }


    // TODO add move functionality
    public void Move()
    {

    }
}

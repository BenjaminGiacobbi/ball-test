using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
public abstract class PowerUpBase : MonoBehaviour
{
    protected abstract void PowerUp(Player player);
    protected abstract void PowerDown();

    // feedback fields
    [SerializeField] ParticleSystem _collectParticles;
    [SerializeField] AudioClip _collectSound;


    // timer fields
    [SerializeField] protected float _powerUpDuration = 5f;
    protected float _timer = 0;


    // object fields
    protected Collider _objectCollider = null;
    protected MeshRenderer _renderer = null;


    // caching for objects
    private void Awake()
    {
        _objectCollider = GetComponent<Collider>();
        _renderer = GetComponent<MeshRenderer>();
    }


    // updates timer independent of framerate
    private void FixedUpdate()
    {
        ManageTimer();
    }


    // tests for player collision and activates power up if player script is recognized
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            // disables object visuals but leaves gameobject active
            _objectCollider.enabled = false;
            _renderer.enabled = false;


            // calls abstract method
            PowerUp(player);
            Feedback();
            _timer = _powerUpDuration;
        }
    }


    // plays feedback -- this is very similar to feedback in CollectibleBase, might be worth combining collectibles and powerups as one base function
    private void Feedback()
    {
        // particles
        if (_collectParticles != null)
        {
            _collectParticles = Instantiate
                (_collectParticles, transform.position, Quaternion.identity);
        }

        // audio
        if (_collectSound != null)
        {
            AudioHelper.PlayClip2D(_collectSound, 1f);
        }
    }


    // counts the timer once activated and deactivates automatically
    private void ManageTimer()
    {
        if(_timer > 0)
        {
            _timer -= Time.deltaTime;
            if(_timer < 0)
            {
                // sets timer to 0 to prevent bugs
                _timer = 0;
                PowerDown();
            }
        }
    }
}

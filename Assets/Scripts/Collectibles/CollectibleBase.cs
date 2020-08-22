using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class CollectibleBase : MonoBehaviour
{
    // function to inherit
    protected abstract void Collect(Player player);


    // fields
    [SerializeField] float _movementSpeed = 1;
    protected float MovementSpeed { get { return _movementSpeed; } }
    [SerializeField] ParticleSystem _collectParticles;
    [SerializeField] AudioClip _collectSound;
    Rigidbody _rb;


    // caching
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Movement(_rb);
    }

    protected virtual void Movement(Rigidbody rb)
    {
        // calcuate rotation
        Quaternion turnOffset = Quaternion.Euler(0, _movementSpeed, 0);
        rb.MoveRotation(_rb.rotation * turnOffset);
    }


    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null)
        {
            // feedback plays first to pass the particle reference to collect
            Feedback();
            Collect(player);

            gameObject.SetActive(false);
        }
    }

    
    private void Feedback()
    {
        // particles
        if (_collectParticles != null)
        {
            _collectParticles = Instantiate
                (_collectParticles, transform.position, Quaternion.Euler(new Vector3(-45, 0, 0)));
        }

        // audio
        if (_collectSound != null)
        {
            AudioHelper.PlayClip2D(_collectSound, 1f);
        }
    }
}

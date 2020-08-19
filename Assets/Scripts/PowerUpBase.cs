using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
public abstract class PowerUpBase : MonoBehaviour
{
    protected abstract void PowerUp(Player player);
    protected abstract void PowerDown(Player player);

    // timer fields
    [SerializeField] float _powerUpDuration = 5f;
    protected float timer = 0;

    // object fields
    protected Collider objectCollider = null;
    protected MeshRenderer renderer = null;

    // player reference to continue to affect player after initial contact
    protected Player playerReference = null;


    // caching for objects
    private void Awake()
    {
        objectCollider = GetComponent<Collider>();
        renderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        ManageTimer();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerReference = other.GetComponent<Player>();
        if(playerReference != null)
        {
            objectCollider.enabled = false;
            renderer.enabled = false;

            PowerUp(playerReference);
            timer = _powerUpDuration;
        }
    }

    // counts the timer once activated and deactivates automatically
    private void ManageTimer()
    {
        if(_powerUpDuration > 0)
        {
            _powerUpDuration -= Time.deltaTime;
            if(_powerUpDuration < 0)
            {
                _powerUpDuration = 0;
                PowerDown(playerReference);
            }
        }
    }
}

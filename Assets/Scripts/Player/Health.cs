using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{

    // health fields
    [SerializeField] int _maxHealth = 3;
    private int _currentHealth;
    private bool _invincible = false;


    // damage feedback
    [SerializeField] Material _damageMaterial = null;
    [SerializeField] AudioClip _damageSound = null;
    [SerializeField] ParticleSystem _deathParticles = null;
    [SerializeField] AudioClip _deathSound = null;

    Coroutine _damageEffect = null;


    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }


    // sets object to invincible
    public void SetInvincible(bool invicibleState)
    {
        _invincible = invicibleState;
    }


    // increments health by set amount
    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        // TODO this should become a UI element, but the deliverables don't focus on it and I know how
        Debug.Log("Player's Health: " + _currentHealth);

    }

    // damages and plays damage feedback
    public void DecreaseHealth(int amount)
    {
        if (!_invincible)
        {
            _currentHealth -= amount;
            // TODO same as above
            Debug.Log("Player's Health: " + _currentHealth);

            if (_currentHealth <= 0)
                Kill();
            else
            {
                if (_damageEffect == null)
                {
                    _damageEffect = StartCoroutine(DamageFlash());
                    AudioHelper.PlayClip2D(_damageSound, 1.8f);
                }
            }
        } 
    }

    
    // deactivates player - forces game over
    public void Kill()
    {
        if (!_invincible)
        {
            // plays death feedback and sets gameobject to inactive
            AudioHelper.PlayClip2D(_deathSound, 1f);
            _deathParticles = Instantiate(
                _deathParticles, transform.position, Quaternion.identity);

            gameObject.SetActive(false);
        }
    }


    // switches object material for a short time for damage effect
    IEnumerator DamageFlash()
    {
        Renderer objectRenderer = GetComponent<Renderer>();
        Material tempMaterial = objectRenderer.material;
        objectRenderer.material = _damageMaterial;

        yield return new WaitForSeconds(0.3f);

        objectRenderer.material = tempMaterial;
        _damageEffect = null;
    }
}

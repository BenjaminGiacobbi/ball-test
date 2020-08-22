using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // TODO offload into a generic health function
    [SerializeField] int _maxHealth = 3;
    private int _currentHealth;
    private bool _invincible = false;

    [SerializeField] Material _damageMaterial = null;
    [SerializeField] AudioClip _damageSound = null;
    [SerializeField] AudioClip _deathSound = null;

    Coroutine _damageEffect = null;


    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void SetInvincible(bool invicibleState)
    {
        _invincible = invicibleState;
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Player's Health: " + _currentHealth);

    }

    public void DecreaseHealth(int amount)
    {
        if (!_invincible)
        {
            _currentHealth -= amount;
            Debug.Log("Player's Health: " + _currentHealth);

            if (_currentHealth < 0)
                Kill();
            else
            {
                if (_damageEffect == null)
                {
                    _damageEffect = StartCoroutine(DamageFlash());
                    AudioHelper.PlayClip2D(_damageSound, 0.5f);
                }
            }
                
        }
    }

    public void Kill()
    {
        if (!_invincible)
        {
            AudioHelper.PlayClip2D(_deathSound, 1f);
            gameObject.SetActive(false);
            // particles
        }
    }

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

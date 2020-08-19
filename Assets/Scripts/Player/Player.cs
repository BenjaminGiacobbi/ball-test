using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BallMotor))]
public class Player : MonoBehaviour
{
    public event Action<int> TreasureCollected = delegate { };

    // TODO offload into a generic health function
    [SerializeField] int _maxHealth = 3;
    private int _currentHealth;
    private int _currentTreasure;
    private bool _invincible = false;



    BallMotor _ballMotor;

    private void Awake()
    {
        _ballMotor = GetComponent<BallMotor>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _currentTreasure = 0;
    }

    private void FixedUpdate()
    {
        ProcessMovement();  
    }

    private void ProcessMovement()
    {
        //TODO move into Input script
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        _ballMotor.Move(movement);
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
        if(!_invincible)
        {
            _currentHealth -= amount;
            Debug.Log("Player's Health: " + _currentHealth);
            if (_currentHealth < 0)
                Kill();
        } 
    }

    public void Kill()
    {
        if (!_invincible)
        {
            gameObject.SetActive(false);
            // particles
            // sound
        }
    }

    public void CollectTreasure(int amount)
    {
        _currentTreasure += amount;
        TreasureCollected?.Invoke(_currentTreasure);
    }
}

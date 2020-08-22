using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BallMotor))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    public event Action<int> TreasureCollected = delegate { };
    private int _currentTreasure = 0;

    BallMotor _ballMotor;

    private void Awake()
    {
        _ballMotor = GetComponent<BallMotor>();
    }

    private void Start()
    {
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

    public void CollectTreasure(int amount)
    {
        _currentTreasure += amount;
        TreasureCollected?.Invoke(_currentTreasure);
    }
}

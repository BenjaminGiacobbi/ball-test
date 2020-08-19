using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : Enemy
{
    [SerializeField] float _bounceForce = 5.0f;

    protected override void PlayerImpact(Player player)
    {
        // perform base function
        base.PlayerImpact(player);

        // get motor and apply opposite force
        BallMotor _motor = player.GetComponent<BallMotor>();
        if(_motor != null)
        {
            Vector3 pushDirection = _motor.transform.position - transform.position;
            _motor.GetComponent<Rigidbody>().AddForce(pushDirection * _bounceForce);
        }
    }
}

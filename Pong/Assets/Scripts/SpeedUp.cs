using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : PowerUp {

    public float AddSpeed = 3.0f;                                           // The added speed variable (default set to +3)

    new void OnTriggerEnter(Collider other)
    {
        BallController ball = other.GetComponent<BallController>();
        StartCoroutine(SpeedUpBall(ball));
        base.OnTriggerEnter(null);
    }

    IEnumerator SpeedUpBall(BallController ball)
    {
        // Increase the ball speed
        ball.SetSpeed(AddSpeed);
        // Wait for a given time
        yield return new WaitForSeconds(powerUpTime);
        // Decrease the ball speed
        ball.SetSpeed(-AddSpeed);

        // Destroy the power up object
        Destroy(gameObject);
    }
}

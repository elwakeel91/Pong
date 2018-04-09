using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float powerUpTime = 5.0f;                    // Power Up timer
    public float deltaSpeed = 3.0f;                     // Increase in speed
    public GameObject powerUpVFX;                       // Power up VFX game object

    /// <summary>
    /// Handles Collisions
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        BallController ball = other.GetComponent<BallController>();
        StartCoroutine(SpeedUp(ball));
    }

    IEnumerator SpeedUp(BallController ball)
    {
        // Disable the graphics and collider
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;

        // Instantiate our VFX
        GameObject vfx = Instantiate(powerUpVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(vfx, 1.0f);

        // Increase the ball speed
        ball.SetSpeed(deltaSpeed);
        // Wait for a given time
        yield return new WaitForSeconds(powerUpTime);
        // Decrease the ball speed
        ball.SetSpeed(-deltaSpeed);

        // Destroy the power up object
        Destroy(gameObject);
    }
	
}

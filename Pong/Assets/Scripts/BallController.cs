﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    #region Public Variables

    public float startSpeed = 1.0f;         // Start speed of the ball (default is 1)
    public float maxSpeed = 5.0f;           // Maximum speed of the ball (default is 5)
    public ParticleSystem trail;            // Particle system containing our ball trail
    public bool IsMoving { get; set; }      // Bool to check if the ball is allowed to move

    #endregion

    #region Private Variables

    // Ball properties
    float speed;                        // The current speed of the ball
    Vector3 direction;                  // The direction we want the ball to move in
    Vector3 startPosition;              // The start position of the ball

    // Unity properties
    Rigidbody rb;                       // Rigid body component of the ball
    AudioSource audioSource;            // Audio Source attached to the ball

    #endregion

    #region Unity Methods

    /// <summary>
    /// Method called when the object is enabled
    /// </summary>
    void Start()
    {
        speed = startSpeed;                             // Initialise the speed
        startPosition = transform.position;             // Initialise the start position
        IsMoving = false;                               // Initialise the movement vector

        rb = GetComponent<Rigidbody>();                 // Set the rigid body component
        audioSource = GetComponent<AudioSource>();      // Set the audio source
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// Method called every physics frame
    /// </summary>
    void FixedUpdate()
    {
        // Check if we want to move the ball
        if (IsMoving)
            // Move the ball
            Move();
    }

    /// <summary>
    /// Handles collisions
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        // Check if we collide with the walls
        if (other.gameObject.CompareTag("Wall"))
        {
            // Change the Y direction vector
            direction = new Vector3(direction.x, -direction.y, 0);

            // Play the collision audio audio
            audioSource.pitch = 1;
            audioSource.Play();
        }

        // Check if we collide with the players
        if (other.gameObject.CompareTag("Player"))
        {
            // Calculate the paddle hit angle
            float yVector = PaddleAngle(other);
            // Change the direction vector
            direction = new Vector3(-direction.x, yVector, 0);
            direction.Normalize();

            // Increase the speed of the ball by 0.25
            speed += 0.25f;
            // Make sure the speed doesn't exceed our max speed
            speed = Mathf.Clamp(speed, 0.0f, maxSpeed);

            // Play the collision audio
            audioSource.pitch = 2;
            audioSource.Play();
        }

        // Check if we collide with the goal
        if (other.gameObject.CompareTag("Goal"))
        {
            // Reset the ball position
            RestartPosition();

            // Access the Game Controller script
            GameObject gameControllerGO = GameObject.FindGameObjectWithTag("GameController");
            GameController gameController = gameControllerGO.GetComponent<GameController>();

            // Check if the ball is in the positive x axis
            if (transform.position.x > 0)
                // Player one scored
                gameController.AddScore(1);
            else // Player two scored
                gameController.AddScore(2);
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Handles restarting the position of the ball
    /// </summary>
    public void RestartPosition()
    {
        // Stop moving the ball
        IsMoving = false;
        rb.velocity = Vector3.zero;
        direction = Vector3.zero;
        // Stop the trail particle system
        trail.Stop();
        // Return to the starting position
        rb.position = startPosition;
    }

    /// <summary>
    /// Handles starting the movement of the ball
    /// </summary>
    public void StartMoving()
    {
        // Start the trail particle system
        trail.Play();
        // Rotate the ball to 90 degrees in the y axis
        direction = Vector3.right;
        // Reset the speed of the ball
        speed = startSpeed;
        // Start moving the ball
        IsMoving = true;
    }

    /// <summary>
    /// Changes the speed by the amount 'deltaSpeed'
    /// </summary>
    /// <param name="deltaSpeed"></param>
    public void SetSpeed (float deltaSpeed)
    {
        // Increase the speed and maximum speed by deltaSpeed
        speed += deltaSpeed;
        maxSpeed += deltaSpeed;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Handles moving the ball towards the positive z axis
    /// </summary>
    void Move()
    {
        // Move the position of the rigid body using the positive z axis and speed
        rb.velocity = direction * speed;
    }

    float PaddleAngle(Collision player)
    {
        // Calculate the distance from the centre of the paddle the ball hit
        float distanceFromCentre = transform.position.y - player.transform.position.y;
        // Calculate the Y direction vector and return it
        return 2 * distanceFromCentre / player.transform.localScale.y;
    }

    #endregion

}

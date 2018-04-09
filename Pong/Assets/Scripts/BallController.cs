using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    #region Public Variables

    public float startSpeed = 1.0f;         // Start speed of the ball (default is 1)
    public float maxSpeed = 5.0f;           // Maximum speed of the ball (default is 5)
    public ParticleSystem trail;            // Particle system containing our ball trail

    public Transform playerOnePosition;     // The position of player one
    public Transform playerTwoPosition;     // The position of player two

    #endregion

    #region Private Variables

    // Ball properties
    float speed;                        // The current speed of the ball
    Vector3 startPosition;              // The start position of the ball
    bool isMoving;                      // Bool to check if the ball is allowed to move

    // Unity properties
    Rigidbody rb;                       // Rigid body component of the ball

    // Other scripts
    GameController gameController;      // Game Controller script

    #endregion

    #region Unity Methods

    /// <summary>
    /// Method called when the object is enabled
    /// </summary>
    void Start()
    {
        speed = startSpeed;                     // Initialise the speed
        startPosition = transform.position;     // Initialise the start position
        isMoving = false;                       // Initialise the movement vector

        rb = GetComponent<Rigidbody>();         // Set the rigid body component
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();     // Set the game controller script
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    void Update()
    {
        // Check if the user has pressed the space bar and the ball isn't moving
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
            // Start moving the ball
            StartMoving();
    }

    /// <summary>
    /// Method called every physics frame
    /// </summary>
    void FixedUpdate()
    {
        // Check if we want to move the ball
        if (isMoving)
            // Move the ball
            Move();
    }

    /// <summary>
    /// Handles collisions
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        // Check if we collide with the players
        if (other.gameObject.CompareTag("Player"))
        {
            // Check which player we collided with (if we are in the positive x then we hit player two)
            Transform playerPosition = (transform.position.x > 0) ? playerTwoPosition : playerOnePosition;

            // Calculate which part of the paddle we hit
            float distanceFromCentre = (transform.position.y - playerPosition.position.y);
            // Calculate which direction the ball should bounce back (up or down)
            float direction = (distanceFromCentre > 0) ? -1.0f : 1.0f;
            // Calculate the angle we get off the paddle (use 125 to get angles ranging from around -50 to 50 degrees)
            float paddleAngle = Mathf.Abs(distanceFromCentre) * 62.5f;
            // Calculate the angle before we hit the paddle
            float angle = (transform.eulerAngles.x >= 300) ? 360 - transform.eulerAngles.x : transform.eulerAngles.x;

            // Calculate the new angle
            float newAngle = angle > paddleAngle ? (angle * direction) : (paddleAngle * direction);
            // Make sure it's clamped between -50 and 50 degrees
            newAngle = Mathf.Clamp(newAngle, -50, 50);         

            // Rotate the direction of the ball
            rb.MoveRotation(Quaternion.Euler(newAngle, -transform.eulerAngles.y, 0));
            // Increase the speed of the ball by 0.25
            speed += 0.25f;
            // Make sure the speed doesn't exceed our max speed
            speed = Mathf.Clamp(speed, 0.0f, maxSpeed);
        }

        // Check if we collide with the walls
        if (other.gameObject.CompareTag("Wall"))
        {
            // Make sure the new angle is between -50 and 50
            float newAngle = Mathf.Clamp(360 - transform.eulerAngles.x, -50, 50);
            // Rotate the direction of the ball 180 degrees in the x axis
            rb.MoveRotation(Quaternion.Euler(newAngle, transform.eulerAngles.y, 0));
        }

        // Check if we collide with the goal
        if (other.gameObject.CompareTag("Goal"))
        {
            // Reset the ball position
            RestartPosition();

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
    /// Handles starting the movement of the ball
    /// </summary>
    public void StartMoving()
    {
        // Start the trail particle system
        trail.Play();
        // Rotate the ball to 90 degrees in the y axis
        rb.MoveRotation(Quaternion.Euler(0, 90, 0));
        // Reset the speed of the ball
        speed = startSpeed;
        // Start moving the ball
        isMoving = true;
    }

    /// <summary>
    /// Handles restarting the position of the ball
    /// </summary>
    public void RestartPosition()
    {
        // Stop moving the ball
        isMoving = false;
        // Pause the trail particle system
        trail.Stop();
        // Return to the starting position
        rb.position = startPosition;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Handles moving the ball towards the positive z axis
    /// </summary>
    void Move()
    {
        // Move the position of the rigid body using the positive z axis and speed
        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    #endregion

}

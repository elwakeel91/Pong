using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    #region public variables

    public float startSpeed = 1.0f;         // Start speed of the ball (default speed is 1)
    public ParticleSystem trail;            // Particle system containing our ball trail

    public Transform playerOnePosition;     // The position of player one
    public Transform playerTwoPosition;     // The position of player two

    #endregion

    #region private variables

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
    /// Initialise our variables
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
            // Rotate the direction of the ball 180 degrees in the y axis
            rb.MoveRotation(Quaternion.Euler(transform.eulerAngles.x, -transform.eulerAngles.y, transform.eulerAngles.z));
        }

        // Check if we collide with the walls
        if (other.gameObject.CompareTag("Wall"))
        {
            // Rotate the direction of the ball 180 degrees in the x axis
            rb.MoveRotation(Quaternion.Euler(-transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
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
        // Rotate the ball 45 degrees in the x axis and 90 degrees in the y axis
        rb.MoveRotation(Quaternion.Euler(45, 90, 0));
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

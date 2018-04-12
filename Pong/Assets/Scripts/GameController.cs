using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    #region Public Variables

    public TextMeshProUGUI playerOneScoreText;         // The UI text for player one's score
    public TextMeshProUGUI playerTwoScoreText;         // The UI text for player two's score

    #endregion

    #region Private Variables

    int playerOneScore;         // Player one's score
    int playerTwoScore;         // Player two's score

    BallController ball;        // The ball controller script attached to our ball game object

    #endregion

    #region Unity Methods

    /// <summary>
    /// Method called when the object is enables
    /// </summary>
    void Start()
    {
        // Initialise the score
        playerOneScore = 0;
        playerTwoScore = 0;
        UpdateScore();

        // Get the ball controller script from the ball game object
        GameObject ballGO = GameObject.FindGameObjectWithTag("Ball");
        ball = ballGO.GetComponent<BallController>();
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    void Update()
    {
        // Check if the user has pressed the space bar and the ball isn't moving
        if (Input.GetKeyDown(KeyCode.Space) && !ball.IsMoving)
            // Start moving the ball
            ball.StartMoving();
    }

    /// <summary>
    /// Method called every physics frame
    /// </summary>
    void FixedUpdate()
    {
        
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds one to the player's score
    /// </summary>
    /// <param name="playerNumber"></param>
    public void AddScore(int playerNumber)
    {
        // Check which player has scored
        switch(playerNumber)
        {
            case 1:
                // Add 1 to player one's score
                playerOneScore++;
                break;
            case 2:
                // Add 1 to player two's score
                playerTwoScore++;
                break;
        }
        // Update the score
        UpdateScore();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Handles updating the score
    /// </summary>
    void UpdateScore()
    {
        // Update player one's score
        playerOneScoreText.text = playerOneScore.ToString();
        // Update player two's score
        playerTwoScoreText.text = playerTwoScore.ToString();
    }

    #endregion
}

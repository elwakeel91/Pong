using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Public Variables

    public float playerStartSpeed = 2;      // The start speed of the players
    public Transform playerOnePosition;     // The position of player one
    public Transform playerTwoPosition;     // The position of player two

    public Text playerOneScoreText;         // The UI text for player one's score
    public Text playerTwoScoreText;         // The UI text for player two's score

    #endregion

    #region Private Variables

    float playerOneSpeed;       // Player one's current speed
    float playerTwoSpeed;       // Player two's current speed
    Vector3 movement;           // The movement vector for the player

    int playerOneScore;         // Player one's score
    int playerTwoScore;         // Player two's score

    #endregion

    #region Unity Methods

    /// <summary>
    /// Method called when the object is enables
    /// </summary>
    void Start()
    {
        // Initialise the speed of the players
        playerOneSpeed = playerTwoSpeed = playerStartSpeed;

        // Initialise the score
        playerOneScore = 0;
        playerTwoScore = 0;
        UpdateScore();
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
        if (Input.GetAxisRaw("Vertical1") != 0)
            MovePlayer(1);
        if (Input.GetAxisRaw("Vertical2") != 0)
            MovePlayer(2);
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
    /// Handles moving the player
    /// </summary>
    /// <param name="playerNumber"></param>
    void MovePlayer(int playerNumber)
    {
        // Switch between the player number
        switch (playerNumber)
        {
            case 1:
                // Set the movement vector using player one's input
                movement = new Vector3(0, playerOneSpeed, 0) * Input.GetAxisRaw("Vertical1");
                // Move the player using the movement vector
                playerOnePosition.Translate(movement * Time.deltaTime);
                break;
            case 2:
                // Set the movement vector using player two's input
                movement = new Vector3(0, playerTwoSpeed, 0) * Input.GetAxisRaw("Vertical2");
                // Move the player using the movement vector
                playerTwoPosition.Translate(movement * Time.deltaTime);
                break;
            default:
                break;
        }
    }

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

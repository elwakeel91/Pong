using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Public Variables

    public int playerNumer = 1;         // The player number (default is set to 1)
    public float startSpeed = 4;        // The start speed of the player (default value is 4)

    #endregion

    #region Private Variables

    private float speed;                // The current speed of the player (might change through power ups)
    private string movementAxis;        // The name of the movement axis from unity (Player one is 'Vertical1', Player 2 is 'Vertical2')
    private float movementInputValue;   // The input value of the movement axis (Ranges from -1 to 1)

    #endregion

    #region Unity Methods

    /// <summary>
    /// Method called when the object is enabled
    /// </summary>
    void Start ()
    {
        // Initialise the speed of the player
        speed = startSpeed;
        // Set the movement axis name
        movementAxis = "Vertical" + playerNumer;
    }
	
    /// <summary>
    /// Method called once every frame
    /// </summary>
    void Update ()
    {
        // Set the input value of the movement axis
        movementInputValue = Input.GetAxisRaw(movementAxis);
    }
	
    /// <summary>
    /// Method called once every physics frame
    /// </summary>
	void FixedUpdate ()
    {
        // Move the player
        Move();
	}

    #endregion

    #region Private Methods

    /// <summary>
    /// Moves the player
    /// </summary>
    void Move ()
    {
        // Set the movement vector
        Vector3 movement = transform.up * movementInputValue * speed * Time.fixedDeltaTime;
        // Move the player using the movement vector
        transform.Translate(movement);
    }

    #endregion
}

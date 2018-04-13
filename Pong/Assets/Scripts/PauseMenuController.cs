using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject PauseMenuUI;

    public static bool isGamePaused = false;

	// Update is called once per frame
	void Update ()
    {
        // Check if the user has pressed the 'Esc' or 'P' keys
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Check if the game is already paused
            if (isGamePaused)
                // Resume
                Resume();
            else // The game isn't paused
                // Pause the game
                Pause();
        }
	}

    void Pause()
    {
        // Pause the movement of the game
        Time.timeScale = 0.0f;
        // Activate the pause menu
        PauseMenuUI.SetActive(true);
        // Set the game to 'Paused' state
        isGamePaused = true;
    }

    public void Resume()
    {
        // Unpause the movement of the game
        Time.timeScale = 1.0f;
        // Activate the pause menu
        PauseMenuUI.SetActive(false);
        // Set the game to 'Paused' state
        isGamePaused = false;
    }

    public void Restart()
    {
        // Unpause the movement of the game
        Time.timeScale = 1.0f;
        // Reload the main game scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        // Unpause the movement of the game
        Time.timeScale = 1.0f;
        // Load the main menu scene
        SceneManager.LoadScene(0);
    }
}

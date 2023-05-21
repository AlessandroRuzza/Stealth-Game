using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    [SerializeField] Camera godViewCamera;
    [SerializeField] Camera playerCamera;

    void Awake()
    {
		isPaused = false;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyBinds.pause) && Player.self.isAlive)
        {
			Debug.Log("Esc Detected");
			Debug.Log(isPaused);
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        godViewCamera.depth = playerCamera.depth - 1;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {

        if(Player.self.isAlive)
        {
            Time.timeScale = 1f;
        }
        pauseMenu.SetActive(false);
        isPaused = false;
            
    }

    public void GoToMainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        Scene current_level = SceneManager.GetActiveScene();

        SceneManager.LoadScene(current_level.buildIndex);
    }
}
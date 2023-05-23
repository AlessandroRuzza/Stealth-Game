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
    bool wasGodview;

    void Awake()
    {
		isPaused = false;
        wasGodview = false;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyBinds.pause) && Player.self.isAlive)
        {
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
        wasGodview = godViewCamera.depth > playerCamera.depth;
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
        if (wasGodview)
        {
            godViewCamera.depth = playerCamera.depth + 1;
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
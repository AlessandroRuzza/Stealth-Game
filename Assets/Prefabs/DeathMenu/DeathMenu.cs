using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenu;
    public static bool isPaused;

	public Animator animator;
	float timer;

    void Awake()
    {
		timer = 0f;
		isPaused = false;
        deathMenu.SetActive(false);
    }

	void Start()
	{
		animator = Player.self.animator;
	}

    void Update()
    {
		if(!Player.self.isAlive)
		{
			timer += Time.unscaledDeltaTime;
			if (!AnimatorIsPlaying() && timer >0.2)
			{
				deathMenu.SetActive(true);
				Time.timeScale = 0f;
			}
		}
    }

    public void PauseGame()
    {
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
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

	bool AnimatorIsPlaying(){
		return animator.GetCurrentAnimatorStateInfo(0).length >
				animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
  	}

}
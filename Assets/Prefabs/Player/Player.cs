using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    int coinsPickedUp;
    [SerializeField] int goal;
    bool endLevel;
    public bool canFinishLevel
    {
        get
        {
            return coinsPickedUp >= goal;
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        endLevel = false;
        coinsPickedUp = 0;
    }

    private void Update()
    {
        if (endLevel && Input.GetKeyDown(KeyCode.R))   // reloads the scene on pressing R (after level ends)
        {
            Time.timeScale = 1;
            int activeIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(activeIndex, LoadSceneMode.Single);
        }
    }

    public void PickUpCoin()
    {
        coinsPickedUp++;
        print("Picked " + coinsPickedUp + " out of " + goal + " coins so far!");
    }

    public void Spotted()
    {
        // stops the level
        Time.timeScale = 0;
        Debug.Log("You lost. Press R to restart");
        endLevel = true;
    }

    public void LevelComplete()
    {
        Debug.Log("You won! Great job. Press R to restart");
        endLevel = true;
    }
}

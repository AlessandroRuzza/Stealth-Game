using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player self;

    int coinsPickedUp;
    public int GetCoins() { return coinsPickedUp; }
    [SerializeField] int goal;
    public int coinsLeft
    {
        get { return goal - coinsPickedUp; }
    }

    public bool endLevel;
    public bool isAlive;
    public bool canFinishLevel
    {
        get{  return coinsLeft <= 0; }
    }

    private void Awake()
    {
        if(self != null)
        {
            Debug.LogError("Multiple Players are present");
        }
        self = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        endLevel = false;
        isAlive = true;
        coinsPickedUp = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene((int)SceneIndexes.mainMenu, LoadSceneMode.Single);
        }

        if (endLevel && Input.GetKeyDown(KeyCode.R))   // reloads the scene on pressing R (after level ends)
        {
            Time.timeScale = 1;
            int activeIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(activeIndex, LoadSceneMode.Single);
        }

        if (endLevel && isAlive && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            int activeIndex = SceneManager.GetActiveScene().buildIndex;
            //Remember to cap maximum active index to number of levels
            SceneManager.LoadScene(activeIndex+1, LoadSceneMode.Single);
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
        isAlive = false;
    }

    public void LevelComplete()
    {
        Debug.Log("You won! Great job. Press R to restart");
        endLevel = true;
    }
}

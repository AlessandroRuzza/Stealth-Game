using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public const string keyPlayerName = "playerName";
    public static Player self;
    public static Action<int> useEye;
    public static string playerName
    {
        get { return PlayerPrefs.GetString(keyPlayerName); }
    }
    public static string folderPath
    {
        get { return Application.persistentDataPath + "/" + playerName + "/"; }
    }

    public Animator animator;

    int coinsPickedUp;
    public int GetCoins() { return coinsPickedUp; }
    [SerializeField] int goal;
    public int totCoins() { return goal;}
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
        if (self != null)
        {
            Debug.LogError("Multiple Players are present");
            return;
        }
        self = this;
        useEye = null;
        animator = GetComponent<Animator>();
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
        /*
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
        }*/

        // Get the current rotation of the object
        Quaternion currentRotation = transform.rotation;

        // Reset the Z rotation to 0
        currentRotation.eulerAngles = new Vector3(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, 0f);

        // Update the rotation of the object
        transform.rotation = currentRotation;
    }

    public void PickUpCoin()
    {
        coinsPickedUp++;
        print("Picked " + coinsPickedUp + " out of " + goal + " coins so far!");
    }

    public void Spotted()
    {
        // stops the level
        if (endLevel) return;
        Time.timeScale = 0;
        Debug.Log("You lost. Press R to restart");
        isAlive = false;
        EndLevel();
    }

    public void LevelComplete()
    {
        if (endLevel) return;
        Debug.Log("You won! Great job. Press R to restart or Space to continue");
        File.Create(folderPath + SceneManager.GetActiveScene().name).Close();
        EndLevel();
    }

    void EndLevel(){
        endLevel = true;
        useEye(-1);     // turns off the eye overlay
    }
}

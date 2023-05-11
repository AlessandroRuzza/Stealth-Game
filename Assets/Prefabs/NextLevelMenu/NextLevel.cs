using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    Player player;
    [SerializeField] GameObject nextLevelMenu;

    void Start()
    {
        player = Player.self;    
    }
    
    void Update()
    {
        if(player.endLevel && player.isAlive)
        {
            nextLevelMenu.SetActive(true);
        }    
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        int activeIndex = SceneManager.GetActiveScene().buildIndex;
        //Remember to cap maximum active index to number of levels
        SceneManager.LoadScene(activeIndex + 1, LoadSceneMode.Single);
    }
    public void LoadLevelSelect()
    {
        SceneManager.LoadScene((int)SceneIndexes.levelSelection);
    }
}

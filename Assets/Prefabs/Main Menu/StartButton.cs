using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum SceneIndexes
{
    mainMenu,
    achievementsReview,
    levelSelection,
    level1,
    level2
}
// Scene 0 is the main menu
// Scene 1 is the achievement menu
// Scene 2 is the first level

public class StartButton : MonoBehaviour
{
    [SerializeField] GameObject playerNameErrorWindow;

    public void LoadFirstLevel()
    {
        string playerName = PlayerPrefs.GetString(ConfirmName.keyPlayerName);
        if(playerName != "" && playerName != null)
            SceneManager.LoadScene((int)SceneIndexes.levelSelection, LoadSceneMode.Single);
        else
            playerNameErrorWindow.SetActive(true);
    }
}

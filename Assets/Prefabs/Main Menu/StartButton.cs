using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum SceneIndexes
{
    mainMenu,
    settingsMenu,
    achievementsReview,
    levelSelection,
    level1,
    level2,
    level3,
    level4
}

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

    public void CloseGame()
    {
        Application.Quit();
    }
}

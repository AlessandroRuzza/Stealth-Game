using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum SceneIndexes
{
    mainMenu,
    achievementsReview,
    level1
}
// Scene 0 is the main menu
// Scene 1 is the achievement menu
// Scene 2 is the first level

public class StartButton : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene((int)SceneIndexes.level1, LoadSceneMode.Single);
    }
}

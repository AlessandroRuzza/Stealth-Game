using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    const int level1SceneIndex = 1;
    // Scene 0 is the main menu
    // Scene 1 is the first level
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(level1SceneIndex, LoadSceneMode.Single);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevelsButton : MonoBehaviour
{
    public void ResetAllLevels()
    {
        LevelManager.self.ResetLevels();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}

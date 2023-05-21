using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    bool sceneLoaded;
    int settingsMenuIndex;
    Scene settingsMenu;

    void Awake()
    {
        sceneLoaded = false;
        settingsMenuIndex = (int)SceneIndexes.settingsMenu;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.sceneCount>1)
        {
            SceneManager.UnloadSceneAsync(settingsMenu);
            sceneLoaded = false;
        }
    }
    
    public void OpenSettingsSceneMenu()
    {
        if(!sceneLoaded && SceneManager.sceneCount==1)
        {
            SceneManager.LoadScene(settingsMenuIndex, LoadSceneMode.Additive);
            settingsMenu = SceneManager.GetSceneAt(SceneManager.sceneCount-1);
            SceneManager.SetActiveScene(settingsMenu);
            sceneLoaded = true;
        }

    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu self;
    bool sceneLoaded;
    public bool canExit;
    int settingsMenuIndex;
    Scene settingsMenu;

    void Awake()
    {
        if (self != null) Debug.LogError("Multiple setting menus! " + name);
        else self = this;
        sceneLoaded = false;
        settingsMenuIndex = (int)SceneIndexes.settingsMenu;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.sceneCount>1 && canExit)
        {
            CloseSettingsSceneMenu();
        }
    }
    
    public void OpenSettingsSceneMenu()
    {
        if(!sceneLoaded && SceneManager.sceneCount==1)
        {
            KeyBinds.LoadAll();
            SceneManager.LoadScene(settingsMenuIndex, LoadSceneMode.Additive);
            settingsMenu = SceneManager.GetSceneAt(SceneManager.sceneCount-1);
            //SceneManager.SetActiveScene(settingsMenu);
            sceneLoaded = true;
        }
    }
    public void CloseSettingsSceneMenu()
    {
        SceneManager.UnloadSceneAsync(settingsMenu);
        sceneLoaded = false;
    }
}

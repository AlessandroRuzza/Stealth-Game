using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public void CloseSettingsMenu()
    {
        SettingsMenu.self.CloseSettingsSceneMenu();
    }
}

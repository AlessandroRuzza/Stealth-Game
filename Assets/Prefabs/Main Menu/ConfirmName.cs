using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ConfirmName : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] PlayerNameDropdown nameDropdown;

    private void Start()
    {
        if(Player.playerName != null)
        {
            nameInputField.text = Player.playerName;
        }
    }

    public void SaveNameToPrefs()
    {
        PlayerPrefs.SetString(Player.keyPlayerName, nameInputField.text);
        PlayerPrefs.Save();

        if (!Directory.Exists(Player.folderPath))
        {
            Directory.CreateDirectory(Player.folderPath);
            nameDropdown.RefreshPlayerList();
        }
        nameDropdown.RefreshPlayerSelected();
    }
}

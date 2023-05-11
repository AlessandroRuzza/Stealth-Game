using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ConfirmName : MonoBehaviour
{
    public const string keyPlayerName = "playerName";
    string playerName; 
    string playerFolderPath
    {
        get { return Application.persistentDataPath + "/" + playerName; }
    }
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] PlayerNameDropdown nameDropdown;

    private void Start()
    {
        playerName = PlayerPrefs.GetString(keyPlayerName);
        if(playerName != null)
        {
            nameInputField.text = playerName;
        }
    }

    public void SaveNameToPrefs()
    {
        playerName = nameInputField.text;
        PlayerPrefs.SetString(keyPlayerName, playerName);
        PlayerPrefs.Save();

        if (!Directory.Exists(playerFolderPath))
        {
            Directory.CreateDirectory(playerFolderPath);
            nameDropdown.RefreshPlayerList();
        }
        nameDropdown.RefreshPlayerSelected();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfirmName : MonoBehaviour
{
    const string keyPlayerName = "playerName";
    string playerName;
    [SerializeField] TMP_InputField nameInputField;

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
    }
}

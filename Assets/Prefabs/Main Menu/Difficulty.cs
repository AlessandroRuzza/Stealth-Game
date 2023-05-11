using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    public const string keyDifficulty = "Difficulty";
    [SerializeField] int difficulty;
    [SerializeField] Color selectedColor;
    Image image; 
    string playerFolderPath
    {
        get { return Application.persistentDataPath + "/" + PlayerPrefs.GetString(ConfirmName.keyPlayerName) + "/"; }
    }

    public static Action reload = null;

    private void Awake()
    {
        image = GetComponent<Image>();
        reload = null;
    }

    void Start()
    {
        reload += SetColor;
        SetColor();
    }

    void SetColor()
    {
        string playerName = PlayerPrefs.GetString(ConfirmName.keyPlayerName);
        if (difficulty == PlayerPrefs.GetInt(keyDifficulty) && playerName != "" && playerName != null)
        {
            image.color = selectedColor;
        }
        else
        {
            image.color = Color.white;
        }
    }

    public void SetDifficulty()
    {
        string playerName = PlayerPrefs.GetString(ConfirmName.keyPlayerName);
        if (playerName == null || playerName == "")
            return;

        try
        {
            File.Delete(playerFolderPath + "Difficulty_1");
            File.Delete(playerFolderPath + "Difficulty_2");
            File.Delete(playerFolderPath + "Difficulty_3");
            File.Create(playerFolderPath + "Difficulty_" + difficulty).Close();
        }
        catch (IOException e) {
            Debug.LogError("Player not initialised! " + e.Message);
            return; 
        }

        PlayerPrefs.SetInt(keyDifficulty, difficulty);
        reload();
    }
}

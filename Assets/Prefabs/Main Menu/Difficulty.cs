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

    // State pattern for difficulty
    private IDifficultyState difficultyState;

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
            // Set the difficulty state based on the selected difficulty
            switch (difficulty)
            {
                case 1:
                    difficultyState = new Difficulty1();
                    break;
                case 2:
                    difficultyState = new Difficulty2();
                    break;
                case 3:
                    difficultyState = new Difficulty3();
                    break;
                default:
                    break;
            }

            // Save the selected difficulty
            difficultyState.SaveDifficulty(playerFolderPath, difficulty);
        }
        catch (IOException e)
        {
            Debug.LogError("Player not initialised! " + e.Message);
            return;
        }

        PlayerPrefs.SetInt(keyDifficulty, difficulty);
        reload();
    }
}

public interface IDifficultyState
{
    void SaveDifficulty(string playerFolderPath, int difficulty);
}

public class Difficulty1 : IDifficultyState
{
    public void SaveDifficulty(string playerFolderPath, int difficulty)
    {
        File.Delete(playerFolderPath + "Difficulty_2");
        File.Delete(playerFolderPath + "Difficulty_3");
        File.Create(playerFolderPath + "Difficulty_1").Close();
    }
}

public class Difficulty2 : IDifficultyState
{
    public void SaveDifficulty(string playerFolderPath, int difficulty)
    {
        File.Delete(playerFolderPath + "Difficulty_1");
        File.Delete(playerFolderPath + "Difficulty_3");
        File.Create(playerFolderPath + "Difficulty_2").Close();
    }
}

public class Difficulty3 : IDifficultyState
{
    public void SaveDifficulty(string playerFolderPath, int difficulty)
    {
        File.Delete(playerFolderPath + "Difficulty_1");
        File.Delete(playerFolderPath + "Difficulty_2");
        File.Create(playerFolderPath + "Difficulty_3").Close();
    }
}

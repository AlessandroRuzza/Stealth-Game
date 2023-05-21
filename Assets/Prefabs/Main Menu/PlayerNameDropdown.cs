using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerNameDropdown : MonoBehaviour
{
    List<string> listOfPlayers; 
    TMP_Dropdown dropdown;
    [SerializeField] TMP_InputField textField;

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        RefreshPlayerList();
        RefreshPlayerSelected();
    }

    public void RefreshPlayerList()
    {
        listOfPlayers = Directory.EnumerateDirectories(Application.persistentDataPath).Append("").ToList();
        // need to append that extra element to make the dropdown display elements correctly

        for(int i=0; i<listOfPlayers.Count; i++)
        {
            listOfPlayers[i] = listOfPlayers[i].Split("\\").Last();
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(listOfPlayers);
    }

    public void RefreshPlayerSelected()
    {
        int index = dropdown.options.FindIndex((TMP_Dropdown.OptionData optionData) => (optionData.text == Player.playerName));
        dropdown.value = index;
        dropdown.RefreshShownValue();
    }

    public void LoadPlayerData()
    {
        UpdateTextField();
        UpdateKeyBinds();
    }

    void UpdateTextField()
    {
        textField.text = dropdown.options[dropdown.value].text;
        PlayerPrefs.SetString(Player.keyPlayerName, textField.text);

        int difficulty=2;   // default difficulty is normal
        if (File.Exists(Application.persistentDataPath + "/" + Player.playerName + "/Difficulty_1")) 
            difficulty = 1;
        else if (File.Exists(Application.persistentDataPath + "/" + Player.playerName + "/Difficulty_3"))
            difficulty = 3;

        PlayerPrefs.SetInt(Difficulty.keyDifficulty, difficulty);

        if (Difficulty.reload != null)
            Difficulty.reload();
    }

    void UpdateKeyBinds()
    {
        KeyBinds.LoadAll();
    }
}

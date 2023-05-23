using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class KeyBinds
{
    static string KeyBindFilePath{
        get { return Player.folderPath + "KeyBinds"; }
    }

    public static KeyCode moveUp;
    const string keyMoveUp = "moveUpKeyBind";
    public static KeyCode moveDown;
    const string keyMoveDown = "moveDownKeyBind";
    public static KeyCode moveLeft;
    const string keyMoveLeft = "moveLeftKeyBind";
    public static KeyCode moveRight;
    const string keyMoveRight = "moveRightKeyBind";
    public static KeyCode pause;
    const string keyPause = "pauseKeyBind";
    public static KeyCode godView;
    const string keyGodView = "godViewKeyBind";
    public static KeyCode rewind;
    const string keyRewind = "rewindKeyBind";

    public static void SaveAll()
    {
        File.Delete(KeyBindFilePath);
        File.Create(KeyBindFilePath).Close();
        StreamWriter writer = new StreamWriter(KeyBindFilePath);
        SaveKeyBind(writer, keyMoveUp, moveUp);
        SaveKeyBind(writer, keyMoveDown, moveDown);
        SaveKeyBind(writer, keyMoveLeft, moveLeft);
        SaveKeyBind(writer, keyMoveRight, moveRight);
        SaveKeyBind(writer, keyPause, pause);
        SaveKeyBind(writer, keyGodView, godView);
        SaveKeyBind(writer, keyRewind, rewind);
        writer.Close();
        PlayerPrefs.Save();
    }
    static void SaveKeyBind(StreamWriter writer, string key, KeyCode value)  // key param is useless, kept in case we restore PlayerPrefs line
    {
        writer.WriteLine((int)value);
        //PlayerPrefs.SetInt(key, (int)value);
    }

    public static void LoadAll()
    {
        if (!File.Exists(KeyBindFilePath))
        {
            SetDefaultValues();
            return;
        }
        StreamReader reader = new StreamReader(KeyBindFilePath);

        moveUp = (KeyCode)int.Parse(reader.ReadLine());
        moveDown = (KeyCode)int.Parse(reader.ReadLine());
        moveLeft = (KeyCode)int.Parse(reader.ReadLine());
        moveRight = (KeyCode)int.Parse(reader.ReadLine());
        pause = (KeyCode)int.Parse(reader.ReadLine());
        godView = (KeyCode)int.Parse(reader.ReadLine());
        rewind = (KeyCode)int.Parse(reader.ReadLine());
        reader.Close();
    }

    public static void SetDefaultValues()
    {
        moveUp = KeyCode.W;
        moveDown = KeyCode.S;
        moveLeft = KeyCode.A;
        moveRight = KeyCode.D;
        pause = KeyCode.Escape;
        godView = KeyCode.G;
        rewind = KeyCode.R;
        SaveAll();
    }
}

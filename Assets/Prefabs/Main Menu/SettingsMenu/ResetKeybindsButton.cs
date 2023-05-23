using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetKeybindsButton : MonoBehaviour
{
    KeyBindButton[] buttons;
    void Awake()
    {
        buttons = FindObjectsOfType<KeyBindButton>();
    }
    public void ResetDefaultValues()
    {
        KeyBinds.SetDefaultValues();
        foreach(KeyBindButton x in buttons)
        {
            x.LoadKeybind();
        }
    }
}

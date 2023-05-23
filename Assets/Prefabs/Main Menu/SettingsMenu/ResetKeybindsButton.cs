using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetKeybindsButton : MonoBehaviour
{
    public void ResetDefaultValues()
    {
        KeyBinds.SetDefaultValues();
        foreach(KeyBindButton x in KeyBindButton.instances)
        {
            x.LoadKeybind();
        }
        StopAllCoroutines();
    }
}

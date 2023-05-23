using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

enum KeybindButtonID
{
    moveUp,
    moveDown,
    moveLeft,
    moveRight,
    pause,
    godView,
    rewind
}

public class KeyBindButton : MonoBehaviour
{
    const string waitingText = "Press any key...";
    public static KeyBindButton[] instances = new KeyBindButton[System.Enum.GetValues(typeof(KeybindButtonID)).Length];
    KeyCode prevKey;
    private TextMeshProUGUI buttonText;
    [SerializeField] KeybindButtonID keybindID;

    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        instances[(int)keybindID] = this;
        SettingsMenu.self.canExit = true;
        LoadKeybind();
    }

    public void StartRebind()
    {
        if (!SettingsMenu.self.canExit) return;
        prevKey = GetKeyFromKeybinds();
        StartCoroutine(WaitForKey());
        buttonText.text = waitingText;
    }

    private IEnumerator WaitForKey()
    {
        while (!Input.anyKeyDown)
        {
            SettingsMenu.self.canExit = false;
            yield return null;
        }

        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                if (CheckForDoubles(keyCode))
                {
                    yield return null; 
                    StartCoroutine(WaitForKey());
                }
                else
                {
                    SaveKeybind(keyCode);
                    buttonText.text = keyCode.ToString();
                    SettingsMenu.self.canExit = true;
                }
                break;
            }
        }
    }

    bool CheckForDoubles(KeyCode key)   // return true if it detects a double assignment of keybind
    {
        if (key == prevKey) return false;
        foreach (KeyBindButton x in instances)
        {
            if (x.GetKeyFromKeybinds() == key)
            {
                return true;
            }
        }
        return false;
    }

    void SaveKeybind(KeyCode key)
    {
        switch (keybindID)
        {
            default:
            case KeybindButtonID.moveUp:
                KeyBinds.moveUp = key;
                break;
            case KeybindButtonID.moveDown:
                KeyBinds.moveDown = key;
                break;
            case KeybindButtonID.moveLeft:
                KeyBinds.moveLeft = key;
                break;
            case KeybindButtonID.moveRight:
                KeyBinds.moveRight = key;
                break;
            case KeybindButtonID.pause:
                KeyBinds.pause = key;
                break;
            case KeybindButtonID.godView:
                KeyBinds.godView = key;
                break;
            case KeybindButtonID.rewind:
                KeyBinds.rewind = key;
                break;
        }
        KeyBinds.SaveAll();
    }
    public void LoadKeybind()
    {
        KeyCode key = GetKeyFromKeybinds();
        buttonText.text = key.ToString();
    }
    KeyCode GetKeyFromKeybinds()
    {
        KeyCode key;
        switch (keybindID)
        {
            default:
            case KeybindButtonID.moveUp:
                key = KeyBinds.moveUp;
                break;
            case KeybindButtonID.moveDown:
                key = KeyBinds.moveDown;
                break;
            case KeybindButtonID.moveLeft:
                key = KeyBinds.moveLeft;
                break;
            case KeybindButtonID.moveRight:
                key = KeyBinds.moveRight;
                break;
            case KeybindButtonID.pause:
                key = KeyBinds.pause;
                break;
            case KeybindButtonID.godView:
                key = KeyBinds.godView;
                break;
            case KeybindButtonID.rewind:
                key = KeyBinds.rewind;
                break;
        }
        return key;
    }
}

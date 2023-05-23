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
    private Button button;
    private TextMeshProUGUI buttonText;
    [SerializeField] KeybindButtonID keybindID;

    private void Start()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        instances[(int)keybindID] = this;
        SettingsMenu.self.canExit = true;
        LoadKeybind();
    }

    public void StartRebind()
    {
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
                KeyBinds.moveUp = keyCode;
                buttonText.text = keyCode.ToString();
                SaveKeybind(keyCode);
                SettingsMenu.self.canExit = true;
                break;
            }
        }
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
        switch (keybindID)
        {
            default:
            case KeybindButtonID.moveUp:
                buttonText.text = KeyBinds.moveUp.ToString();
                break;
            case KeybindButtonID.moveDown:
                buttonText.text = KeyBinds.moveDown.ToString();
                break;
            case KeybindButtonID.moveLeft:
                buttonText.text = KeyBinds.moveLeft.ToString();
                break;
            case KeybindButtonID.moveRight:
                buttonText.text = KeyBinds.moveRight.ToString();
                break;
            case KeybindButtonID.pause:
                buttonText.text = KeyBinds.pause.ToString();
                break;
            case KeybindButtonID.godView:
                buttonText.text = KeyBinds.godView.ToString();
                break;
            case KeybindButtonID.rewind:
                buttonText.text = KeyBinds.rewind.ToString();
                break;
        }
    }
}

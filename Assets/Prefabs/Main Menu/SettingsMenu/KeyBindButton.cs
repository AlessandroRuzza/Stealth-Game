using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindButton : MonoBehaviour
{
    public string action; // The action that this keybind controls, e.g. "MoveUp"
    private Button button;
    private Text buttonText;

    private void Start()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<Text>();

        // Load the current keybind
        buttonText.text = PlayerPrefs.GetString(action, "W"); // Default to "W"

        button.onClick.AddListener(StartRebind);
    }

    private void StartRebind()
    {
        StartCoroutine(WaitForKey());
    }

    private IEnumerator WaitForKey()
    {
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                PlayerPrefs.SetString(action, keyCode.ToString());
                buttonText.text = keyCode.ToString();
                break;
            }
        }
    }
}

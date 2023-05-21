using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyBindButton : MonoBehaviour
{
    const string waitingText = "Press any key...";
    private Button button;
    private TextMeshProUGUI buttonText;

    private void Start()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        // Load the current keybind
        buttonText.text = KeyBinds.moveUp.ToString();

        button.onClick.AddListener(StartRebind);
    }

    private void StartRebind()
    {
        StartCoroutine(WaitForKey());
        buttonText.text = waitingText;
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
                KeyBinds.moveUp = keyCode;
                buttonText.text = keyCode.ToString();
                KeyBinds.SaveAll();
                break;
            }
        }
    }
}

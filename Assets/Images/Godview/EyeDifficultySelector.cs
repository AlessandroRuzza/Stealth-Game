using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeDifficultySelector : MonoBehaviour
{
    [SerializeField] GameObject easyEyes;
    [SerializeField] GameObject normalEyes;
    [SerializeField] GameObject hardEyes;
    void Start()
    {
        int difficulty = PlayerPrefs.GetInt(Difficulty.keyDifficulty);

        switch (difficulty)
        {
            case 1:
                easyEyes.SetActive(true);
                normalEyes.SetActive(false);
                hardEyes.SetActive(false);
                Player.useEye += DisableEasy;
                break;

            default:
            case 2:
                PlayerPrefs.SetInt(Difficulty.keyDifficulty, 2);
                easyEyes.SetActive(false);
                normalEyes.SetActive(true);
                hardEyes.SetActive(false);
                break;

            case 3:
                easyEyes.SetActive(false);
                normalEyes.SetActive(false);
                hardEyes.SetActive(true);
                Player.useEye += DisableHard;
                break;

        }
    }

    void DisableEasy(int n)
    {
        if(n<0)
            easyEyes.SetActive(false);
    }
    void DisableHard(int n)
    {
        if(n<0)
            hardEyes.SetActive(false);
    }
}

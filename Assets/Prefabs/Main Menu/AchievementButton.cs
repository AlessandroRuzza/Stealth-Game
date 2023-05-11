using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementButton : MonoBehaviour
{
    [SerializeField] GameObject playerNameErrorWindow;

    public void LoadAchiementReview()
    {
        string playerName = PlayerPrefs.GetString(ConfirmName.keyPlayerName);
        if (playerName != "" && playerName != null)
            SceneManager.LoadScene((int)SceneIndexes.achievementsReview, LoadSceneMode.Single);
        else
            playerNameErrorWindow.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementButton : MonoBehaviour
{
    public void LoadAchiementReview()
    {
        SceneManager.LoadScene((int)SceneIndexes.achievementsReview, LoadSceneMode.Single);
    }
}

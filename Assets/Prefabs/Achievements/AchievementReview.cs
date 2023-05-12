using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementReview : MonoBehaviour
{
    [SerializeField] int index;
    AchievementManager achievementManager;

    [SerializeField] TMPro.TextMeshProUGUI title;
    //[SerializeField] TMPro.TextMeshProUGUI description;
    [SerializeField] Image completionBox;
    [SerializeField] Color notDone;
    [SerializeField] Color done;

    void Start()
    {
        achievementManager = AchievementManager.self;
        Achievement a = achievementManager.GetAchievement(index);
        if (a == null) return;
        title.text = a.name;
        //description.text = a.wasCompleted ? "Done" : "Not done";

        completionBox.color = a.wasCompleted ? done : notDone;
    }
}

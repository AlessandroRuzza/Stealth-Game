using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementWindow : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI message;
    [SerializeField] TMPro.TextMeshProUGUI subText;

    AchievementManager achievementManager;

    bool slideIn;
    [SerializeField] float slideSpeed;
    [SerializeField] float leftBound;
    [SerializeField] float rightBound;
    [SerializeField] float timerToSlideOut;
    bool startTimer;
    float time;
    bool timerPassed;

    private void Awake()
    {
        slideIn = false;
    }
    private void Start()
    {
        achievementManager = AchievementManager.self;
    }

    public void Show(Achievement a)
    {
        message.text = a.name;
        subText.text = achievementManager.GetNumOfCompletedAchievements() + " out of " + AchievementManager.NUM_OF_ACHIEVEMENTS;
        slideIn = true;
    }

    void Update()
    {
        if (slideIn)
        {
            transform.Translate(Vector3.left * slideSpeed * Time.unscaledDeltaTime);
            if(transform.position.x <= leftBound)
            {
                slideIn = false;
                time = 0;
                startTimer = true;
            }
        }
        else if(timerPassed)
        {
            transform.Translate(Vector3.left * slideSpeed * Time.unscaledDeltaTime);
            if (transform.position.x >= rightBound)
            {
                timerPassed = false;
            }
        }

        if (startTimer)
        {
            time += Time.deltaTime;
            if(time >= timerToSlideOut)
            {
                timerPassed = true;
                startTimer = false;
            }
        }
    }

}

using System;
using System.Collections;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

public class Achievement
{
    public string key { get; private set; }
    public string name { get; private set; }
    public bool wasCompleted;
    public delegate bool Condition();
    public Condition isRequirementMet;

    public Achievement(string k, string n, Condition condition)
    {
        key = k;
        name = n;
        wasCompleted = false;
        isRequirementMet = condition;
    }

}

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager self;

    public const int NUM_OF_ACHIEVEMENTS=5;
    Achievement[] achievements;
    [SerializeField] bool reviewMode;
    Player player;

    [SerializeField] AchievementWindow achievementWindow;

    private void Awake()
    {
        if (self != null) Debug.LogError("More than 1 achievement manager!");
        self = this;
        achievements = new Achievement[NUM_OF_ACHIEVEMENTS];
        InitAchievements();
    }

    void Start()
    {
        player = Player.self;
    }

    void Update()
    {
        if (reviewMode) return;
        foreach(Achievement a in achievements)
        {
            if (!a.wasCompleted && a.isRequirementMet())
            {
                Debug.Log("Achievement " + a.name + " done!");
                a.wasCompleted = true;
                achievementWindow.Show(a);
                PlayerPrefs.SetInt(a.key, 1);
            }
        }
    }

    public int GetNumOfCompletedAchievements()
    {
        int num = 0;
        foreach (Achievement a in achievements)
        {
            if (a.wasCompleted) num++;
        }
        return num;
    }

    public Achievement GetAchievement(int index)
    {
        if (index > NUM_OF_ACHIEVEMENTS || index <= 0)
        {
            Debug.LogError("Out of Bounds Index...");
            return null;
        }
        if (achievements[index-1] == null)
            InitAchievements();

        return achievements[index-1];
    }

    public void InitAchievements()
    {
        // Achievement 1:  Die when you're only missing one coin
        achievements[0] = new Achievement(
            "Achievement1",
            "Almost there!", 
            () => {
                return player.coinsLeft == 1 && player.endLevel && !player.isAlive;
            });

        // Achievement 2:  Die really quickly
        int quickDeathMaxTime = 10;   // 10 seconds
        achievements[1] = new Achievement(
            "Achievement2",
            "Quick and Painless",
            () => {
                return Time.timeSinceLevelLoad <= quickDeathMaxTime && player.endLevel && !player.isAlive;
            });

        // Achievement 3:  Finish the level in a long time
        int slowLevelCompletionTime = 120;  // 120 seconds
        achievements[2] = new Achievement(
            "Achievement3",
            "Slow And Steady",
            () => {
                return Time.timeSinceLevelLoad >= slowLevelCompletionTime && player.canFinishLevel && player.endLevel && player.isAlive;
            });

        // Achievement 4:  Speedrun the level
        int fastLevelCompletionTime = 30;   // 30 seconds
        achievements[3] = new Achievement(
            "Achievement4",
            "As Swift as the Wind",
            () => {
                return Time.timeSinceLevelLoad <= fastLevelCompletionTime && player.canFinishLevel && player.endLevel && player.isAlive;
            });

        // Achievement 5:  Observe your enemy carefully before starting to play the level
        int minWaitTime = 60;   // 60 seconds
        achievements[4] = new Achievement(
            "Achievement5",
            "Careful Observer",
            () => {
                return Time.timeSinceLevelLoad >= minWaitTime && player.GetCoins() == 0 && player.isAlive;
            });

        foreach (Achievement a in achievements)
        {
            if (PlayerPrefs.HasKey(a.key))
                a.wasCompleted = PlayerPrefs.GetInt(a.key) == 1;
        }
    }

    public void ResetAchievements()
    {
        foreach (Achievement a in achievements)
        {
            PlayerPrefs.SetInt(a.key, 0);
        }
    }
}
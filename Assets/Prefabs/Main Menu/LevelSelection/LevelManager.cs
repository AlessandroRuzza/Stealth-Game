using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

public class Level
{
    public string key;
    public string name;
    public bool wasCompleted;

    public Level(string k, string name)
    {
        key = k;
        this.name = name;
        wasCompleted = false;
    }
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager self;

    public const int NUM_OF_LEVELS=4;
    Level[] levels;

    private void Awake()
    {
        if (self != null) Debug.LogError("More than 1 achievement manager!");
        self = this;


        levels = new Level[NUM_OF_LEVELS];
        InitLevels();

    }

    public int GetNumOfCompletedLevels()
    {
        int num = 0;
        foreach (Level l in levels)
        {
            if (l.wasCompleted) num++;
        }
        return num;
    }

    public Level GetNextLevelToPlay()
    {
        for(int i=0; i<levels.Length; i++)
        {
            if (!levels[i].wasCompleted)
                return levels[i];
        }
        return levels[levels.Length-1];
    }

    public Level GetLevel(int index)
    {
        if (index > NUM_OF_LEVELS || index <= 0)
        {
            Debug.LogError("Out of Bounds Index...");
            return null;
        }
        if (levels[index-1] == null)
            InitLevels();

        return levels[index-1];
    }

    void InitLevels()
    {
        levels[0] = new Level(
            "Level1",
            "Learning the Ropes"
            );
        levels[1] = new Level(
            "Level2",
            "Sneaky Box"
            );
        levels[2] = new Level(
            "Level3",
            "Slippery Slopes"
            );
        levels[3] = new Level(
            "Level4",
            "Evasive Maneuvers"
            );

        foreach (Level l in levels)
        {
            l.wasCompleted = File.Exists(Player.folderPath + l.key);
        }
    }

    public void ResetLevels()
    {
        foreach (Level l in levels)
        {
            File.Delete(Player.folderPath + l.key);
        }
    }
}

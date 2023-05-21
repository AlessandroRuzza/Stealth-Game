using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelReview : MonoBehaviour
{
    [SerializeField] int index;
    LevelManager levelManager;
    Level thisLevel;
    bool canPlay;

    [SerializeField] TMPro.TextMeshProUGUI title;
    Image background;
    [SerializeField] Color notDone;
    [SerializeField] Color almostDone;
    [SerializeField] Color done;

    private void Awake()
    {
        background = GetComponent<Image>();
        canPlay = false;
    }

    void Start()
    {
        levelManager = LevelManager.self;
        thisLevel = levelManager.GetLevel(index);
        if (thisLevel == null) return;
        title.text = thisLevel.name;
        canPlay = thisLevel.wasCompleted;
        background.color = thisLevel.wasCompleted ? done : notDone;

        if (levelManager.GetNextLevelToPlay() == thisLevel)
        {
            background.color = thisLevel.wasCompleted ? done : almostDone;
            canPlay = true;
        }
    }

    public void PlayLevel()
    {
        if(canPlay)
            SceneManager.LoadScene(thisLevel.key);
    }
}

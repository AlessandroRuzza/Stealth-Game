using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]public float timeLeft;
    public bool timerOn = false;

    public TextMesh timerText;

    void Awake(){
        timerText = GetComponent<TextMesh>();
    }
    // Start is called before the first frame update
    void Start()
    {
        timerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(timerOn){
            if(timeLeft>0){
                timeLeft -= Time.deltaTime;
                UpdateTime(timeLeft);
            } else {
                Debug.Log("Time is up!");
                timeLeft = 0;
                timerOn = false;
            }
        }
    }

    public void UpdateTime(float currentTime){
        currentTime++;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00} : {1:00}",minutes,seconds);
    }

}

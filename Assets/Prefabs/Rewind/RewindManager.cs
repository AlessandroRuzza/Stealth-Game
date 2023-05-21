using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindManager : MonoBehaviour
{

    public static bool isRewinding = false;
    //public bool RewindingStatus() { return isRewinding;}
    List<PosTracker> positions;
    const float rewindTime = 5f;
    void Start()
    {
        positions = new List<PosTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyBinds.rewind))
            StartRewind();
        if(Input.GetKeyUp(KeyBinds.rewind))
            StopRewind();
    }

    void FixedUpdate()
    {
        if(isRewinding)
            Rewind();
        else
            Record();
    }

    void Record()
    {
        if(positions.Count>Mathf.Round(rewindTime/Time.fixedDeltaTime))
        {
            positions.RemoveAt(positions.Count-1);
        }
        positions.Insert(0,new PosTracker(transform.position,transform.rotation));
    }

    void Rewind()
    {
        if(positions.Count>0 && Player.self.isAlive)
        {
            PosTracker posTracker = positions[0];
            transform.position = posTracker.position;
            transform.rotation = posTracker.rotation;
            positions.RemoveAt(0);
        } 
        else
        {
            StopRewind();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
    }

}

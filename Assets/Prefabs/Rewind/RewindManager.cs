using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RewindManager : MonoBehaviour
{

    public static bool isRewinding = false;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] bool manageVideo;
    [SerializeField] bool isEnemy;
    EnemyMover enemyMover;
    //public bool RewindingStatus() { return isRewinding;}
    List<PosTracker> positions;
    const float rewindTime = 5f;
    void Start()
    {
        positions = new List<PosTracker>();
        if(manageVideo)
            videoPlayer.Stop();
        if (isEnemy)
            enemyMover = GetComponent<EnemyMover>();
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
        PosTracker pos;
        if (isEnemy)
        {
            pos = new PosTracker(transform.position, transform.rotation,
                enemyMover.counter, enemyMover.travelDirection,
                enemyMover.pathDirection, enemyMover.cumulativeAngle, enemyMover.totalAngle
                );
        }
        else pos = new PosTracker(transform.position, transform.rotation, 0, 1, Vector3.zero, 0, 0);
        positions.Insert(0,pos);
    }

    void Rewind()
    {
        if(positions.Count>0 && Player.self.isAlive)
        {
            PosTracker posTracker = positions[0];
            transform.position = posTracker.position;
            transform.rotation = posTracker.rotation;
            if (isEnemy)
            {
                enemyMover.counter = posTracker.counter;
                enemyMover.travelDirection = posTracker.travelDirection;
                enemyMover.pathDirection = posTracker.pathDirection;
                enemyMover.cumulativeAngle = posTracker.cumulativeAngle;
                enemyMover.totalAngle = posTracker.totalAngle;
            }
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
        if(manageVideo)
            videoPlayer.Play();
    }

    public void StopRewind()
    {
        isRewinding = false;
        if(manageVideo)
            videoPlayer.Stop();
    }

}

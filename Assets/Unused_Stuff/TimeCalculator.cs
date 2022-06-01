using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Attach this script to the parent of the PathNodes: it will create lines to join them 
 * and print in Console the timing to move between them with Timeline
 */
public class TimeCalculator : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool drawLines;

    Transform[] pathNodes = null;
    int childCount;
    GameObject parent;
    void Start()
    {
        childCount = transform.childCount;
        pathNodes = new Transform[childCount];
        for(int i=0; i < childCount; i++)
        {
            pathNodes[i] = transform.GetChild(i);
        }
        parent = Instantiate<GameObject>(new GameObject("DebugLines"));
    }

    void Update()
    {
        if (!drawLines || pathNodes == null) return;

        string message = "";
        float time=0;
        for(int i=0; i < childCount-1; i++)
        {
            Vector3 start = pathNodes[i].position;
            Vector3 end = pathNodes[i + 1].position;
            Debug.DrawLine(start, end, Color.white, 0.01f);

            time += Vector3.Distance(start, end) / speed;
            float angle = Vector3.Angle(start - end, Vector3.up);     

            message += (i+1) + ": " + time + "s ; " + angle + "°   ";
        }
        Debug.Log(message);
    }
}

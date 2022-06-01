using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] Transform path;
    Transform[] pathNodes;

    [SerializeField] float speed;
    [SerializeField] float movePrecision;
    [SerializeField] float rotationSpeed;
    [SerializeField] float anglePrecision;
    [SerializeField] float minAngleThreshold;
    int counter = 1;
    int travelDirection = 1;
    Vector2 moveDirection;
    Vector2 pathDirection;
    float rotationTime, angle, lastAngle=0f;
    void Awake()
    {
        pathNodes = new Transform[path.childCount];
        for(int i=0; i < path.childCount; i++){
            pathNodes[i] = path.GetChild(i);
        }
    }

    void Start()
    {
        UpdateDirection();
    }

    void Update()
    {
        moveDirection = pathNodes[counter].position - transform.position;
        angle = 0;
        float angleThreshold = anglePrecision * lastAngle * Time.deltaTime / rotationTime;
        bool headingOK = Vector2.Angle(moveDirection, transform.up) <= Mathf.Clamp(angleThreshold, minAngleThreshold, Mathf.Infinity);
        bool isInBounds_2Positions = (counter > 1 && travelDirection == 1) || (counter < pathNodes.Length - 2 && travelDirection == -1);
        bool isComingFromEndNode = ((counter == 1 && travelDirection == 1) || (counter == pathNodes.Length - 2 && travelDirection == -1));

        if (isComingFromEndNode && !headingOK) 
            angle = 180;
        else if (!headingOK && isInBounds_2Positions)
        {
            Vector2 oldDirection = pathNodes[counter - travelDirection].position - pathNodes[counter - 2*travelDirection].position;
            angle = Vector2.SignedAngle(oldDirection, pathDirection);
        }

        transform.Rotate(0, 0, angle * Time.deltaTime / rotationTime);
        transform.Translate(moveDirection.normalized * Time.deltaTime * speed, Space.World);

        bool isCloseEnough = moveDirection.magnitude <= movePrecision * Time.deltaTime * speed;
        if (isCloseEnough)
        {   // I am very close to current destination, move to next one
            counter += travelDirection;
            if (counter >= pathNodes.Length || counter < 0)
            {   // if I would be going past the last node in the array, invert direction
                travelDirection = -travelDirection;
                counter += 2 * travelDirection;
            }
            UpdateDirection();
        }
        lastAngle = angle;
    }

    void UpdateDirection()
    {
        pathDirection = pathNodes[counter].position - pathNodes[counter - travelDirection].position;
        rotationTime = (pathDirection.magnitude / speed) / rotationSpeed;
    }
}

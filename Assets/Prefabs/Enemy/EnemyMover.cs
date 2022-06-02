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
    [SerializeField] bool turnClockwise;
    [SerializeField] float anglePrecision;
    [SerializeField] float minAngleThreshold;
    int counter;
    int travelDirection = 1;
    [SerializeField] [Tooltip("Do not set this above pathNode length")] 
        int startNode;
    Vector2 moveDirection;
    Vector2 pathDirection;
    bool isLoop=false;
    float rotationTime, angle, lastAngle=0f;
    void Awake()
    {
        pathNodes = new Transform[path.childCount];
        for(int i=0; i < path.childCount; i++){
            pathNodes[i] = path.GetChild(i);
        }
        if (!IsOffsetOK())
            startNode = 0;
    }
    bool IsOffsetOK()
    {
        if (startNode >= pathNodes.Length)
            Debug.LogError(name + " has an offset too high!");
        else if (startNode < 0)
            Debug.LogError(name + " has a negative offset!");
        else
            return true;

        return false; // Only executed after one of the LogErrors
    }

    void Start()
    {
        counter = 1 + startNode;
        UpdateTravelDirection();
        UpdatePathDirection();
    }

    void Update()
    {
        moveDirection = pathNodes[counter].position - transform.position;

        angle = 0;
        float angleThreshold = anglePrecision * lastAngle * Time.deltaTime / rotationTime;
        bool headingOK = Vector2.Angle(moveDirection, transform.up) <= Mathf.Clamp(angleThreshold, minAngleThreshold, Mathf.Infinity);
        bool isInBounds_2Positions = (counter > 1 && travelDirection == 1) || (counter < pathNodes.Length - 2 && travelDirection == -1);
        bool isComingFromEndNode = ((counter == 1 && travelDirection == 1) || (counter == pathNodes.Length - 2 && travelDirection == -1));

        if (!isLoop && isComingFromEndNode && !headingOK) 
            angle = (turnClockwise) ? -180 : 180;
        else if (!headingOK && isInBounds_2Positions)
        {
            Vector2 oldDirection = pathNodes[counter - travelDirection].position - pathNodes[counter - 2*travelDirection].position;
            angle = Vector2.SignedAngle(oldDirection, pathDirection);
        }
        else if(!headingOK && isLoop && counter == 1)
        {
            Vector2 oldDirection = pathNodes[0].position - pathNodes[pathNodes.Length-2].position;
            angle = Vector2.SignedAngle(oldDirection, pathDirection);
        }

        transform.Rotate(0, 0, angle * Time.deltaTime / rotationTime);
        transform.Translate(moveDirection.normalized * Time.deltaTime * speed, Space.World);

        bool isCloseEnough = moveDirection.magnitude <= movePrecision * Time.deltaTime * speed;
        if (isCloseEnough)
        {   // I am very close to current destination, move to next one
            counter += travelDirection;
            UpdateTravelDirection();
            UpdatePathDirection();
        }
        lastAngle = angle;
    }

    void UpdateTravelDirection()
    {
        if (counter >= pathNodes.Length || counter < 0)
        {   // if I would be going past the last node in the array, invert direction
            if (pathNodes[pathNodes.Length - 1].position != pathNodes[0].position)
            { // the path isn't a loop
                travelDirection = -travelDirection;
                counter += 2 * travelDirection;
                isLoop = false;
            }
            else
            { // path is a loop: no need to invert travelDirection
                counter = 1;
                isLoop = true;
            }
        }
    }
    void UpdatePathDirection()
    {
        pathDirection = pathNodes[counter].position - pathNodes[counter - travelDirection].position;
        rotationTime = (pathDirection.magnitude / speed) / rotationSpeed;
    }
}

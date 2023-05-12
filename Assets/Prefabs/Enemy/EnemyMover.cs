using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] Transform path;
    bool isPathEmpty;
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
    float angle, cumulativeAngle = 0f, totalAngle=0f;

    public Animator animator;
    void Awake()
    {
        isPathEmpty = path == null;
        if (isPathEmpty) return;

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
        if (isPathEmpty) return;

        counter = 1 + startNode;
        UpdateTravelDirection();
        UpdatePathDirection();
    }

    void Update()
    {
        if (isPathEmpty) return;
        moveDirection = pathNodes[counter].position - transform.position;

	angle = 0f;
        float remainingAngle = Vector2.SignedAngle(transform.up, pathDirection);
        bool headingOK = cumulativeAngle >= Mathf.Abs(totalAngle) - anglePrecision;
        bool isInBounds_2Positions = (counter > 1 && travelDirection == 1) || (counter < pathNodes.Length - 2 && travelDirection == -1);
        bool isComingFromEndNode = ((counter == 1 && travelDirection == 1) || (counter == pathNodes.Length - 2 && travelDirection == -1));

        if (!headingOK)
        {
            angle = totalAngle * Time.deltaTime * rotationSpeed;
            transform.Rotate(0, 0, angle);
        }
        else
        {
            transform.Rotate(0, 0, remainingAngle);
        }
        transform.Translate(moveDirection.normalized * Time.deltaTime * speed, Space.World);

        bool isCloseEnough = moveDirection.magnitude <= movePrecision * Time.deltaTime * speed;
        if (isCloseEnough)
        {   // I am very close to current destination, move to next one
            counter += travelDirection;
            UpdateTravelDirection();
            UpdatePathDirection();
            totalAngle = Vector2.SignedAngle(transform.up, pathDirection);
            turnClockwise = (totalAngle > 0);
            cumulativeAngle = 0f;
        }
        else
            cumulativeAngle += Mathf.Abs(angle);
    }

    //void SetTotalAngle()
    //{
    //    totalAngle = Vector2.SignedAngle(transform.up, pathDirection);
    //}
    //void SetRotationDirection()
    //{
    //    turnClockwise = (totalAngle > 0);
    //}

    void UpdateTravelDirection()
    {
        if (counter >= pathNodes.Length || counter < 0)
        {   // if I would be going past the last node in the array, invert direction
	        isLoop = pathNodes[pathNodes.Length - 1].position == pathNodes[0].position;
            if (!isLoop)
            {   // the path isn't a loop
                travelDirection = -travelDirection;
                counter += 2 * travelDirection;
            }
            else
            {   // path is a loop: no need to invert travelDirection
                counter = 1;
            }
        }
    }
    void UpdatePathDirection()
    {
        pathDirection = pathNodes[counter].position - pathNodes[counter - travelDirection].position;
        animator.SetFloat("speed",pathDirection.magnitude);
    }
}

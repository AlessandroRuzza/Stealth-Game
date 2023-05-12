using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] Transform path;
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] int startNode;
    [SerializeField] bool isLoop;

    private Transform[] pathNodes;
    private int currentPoint;
    private bool reverseDirection = false;

    void Awake()
    {
        if (path == null)
        {
            Debug.LogError("Path is not assigned!");
            enabled = false;
            return;
        }

        pathNodes = new Transform[path.childCount];
        for (int i = 0; i < path.childCount; i++)
        {
            pathNodes[i] = path.GetChild(i);
        }
    }

    void Start()
    {
        currentPoint = startNode;
    }

    void Update()
    {
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        Transform targetNode = pathNodes[currentPoint];
        Vector2 directionToTarget = (targetNode.position - transform.position).normalized;

        // Rotate towards target
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;
        float rotationStep = rotationSpeed * Time.deltaTime;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationStep);

        // Move towards target
        float distanceToTarget = Vector2.Distance(transform.position, targetNode.position);
        float moveStep = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetNode.position, moveStep);

        // Check if we reached the target node
        if (Vector2.Distance(transform.position, targetNode.position) < 0.1f)
        {
            if (isLoop)
            {
                if (reverseDirection)
                {
                    currentPoint--;
                    if (currentPoint < 0)
                    {
                        reverseDirection = false;
                        currentPoint = 1;
                    }
                }
                else
                {
                    currentPoint++;
                    if (currentPoint >= pathNodes.Length)
                    {
                        reverseDirection = true;
                        currentPoint = pathNodes.Length - 2;
                    }
                }
            }
            else
            {
                currentPoint = (currentPoint + 1) % pathNodes.Length;
            }
        }
    }
}

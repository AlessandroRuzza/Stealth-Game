using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] Transform path;
    Transform[] pathNodes;

    [SerializeField] float speed = 4f;
    [SerializeField] float rotationSpeed = 2f;
    int counter = 0;
    int travelDirection = 1;
    void Awake()
    {
        pathNodes = new Transform[path.childCount];
        for(int i=0; i < path.childCount; i++){
            pathNodes[i] = path.GetChild(i);
        }
    }

    void Update()
    {
        Vector2 direction = pathNodes[counter].position - transform.position;
        float angle = Vector2.SignedAngle(transform.up, direction);
        Debug.Log(direction.normalized);
        transform.Rotate(0, 0, angle);
        transform.Translate(transform.up * Time.deltaTime * speed * travelDirection);

        if (counter >= pathNodes.Length || counter < 0)
        {   // if I would be going past the last node in the array, invert direction
            travelDirection = -travelDirection;
            Debug.Log("Bound reached, Inverted direction");
        }
        if (direction.magnitude < Time.deltaTime * speed)
        {   // I am very close to current node, move to next one
            counter += travelDirection;
            Debug.Log("Next is " + counter);
        }
        if (counter >= pathNodes.Length || counter < 0)
        {   // if I would be going past the last node in the array, invert direction
            travelDirection = -travelDirection;
            counter += 2 * travelDirection;
            Debug.Log("Bound reached, Inverted direction ; next is " + counter);
        }

    }
}

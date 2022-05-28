using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCone : MonoBehaviour
{
    [SerializeField] int raycastLength = 5;
    float degIncrement = 0.5f;
    float coneAngle = 15f;
    void Awake()
    {

    }

    void Update()
    {
        for(float deg=-coneAngle; deg < coneAngle; deg+=degIncrement){
            Vector2 direction = Quaternion.Euler(0,0,deg)*transform.up;
            direction = direction.normalized;
            float distance = CastRay(direction);
            //Debug.DrawRay(transform.position, direction*distance, Color.white, 0.01f);
        }
    }

    float CastRay(Vector2 direction){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastLength);
        if(hit.collider != null){
            if(hit.collider.tag == "Player"){
                Debug.Log("Player found!"); 
            }
            return hit.distance;
        }
        else
            return raycastLength;
    }
}

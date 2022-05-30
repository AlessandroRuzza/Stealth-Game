using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCone : MonoBehaviour
{
    [SerializeField] int raycastLength = 5;
    [SerializeField] float coneAngle = 15f;
    [SerializeField] int rayCount = 10;
    [SerializeField] bool debug = true;
    //float currentAngle = 0;
    float angleIncrease {
        get{ return 2*coneAngle / rayCount; }
    } 

    void Awake()
    {

    }

    void Start()
    {
    
    }

    void Update()
    {
        //Made test Mesh
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector2 currentPosition = transform.position;
        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[rayCount + 2];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = Vector3.zero;

        int vertexIndex = 1;
        int triangleIndex = 0;
        float angle = coneAngle;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector2 direction = Quaternion.Euler(0,0,angle)*transform.up;
            
            RaycastHit2D raycastHit2D = Physics2D.Raycast(currentPosition, direction, raycastLength);
            if(debug) Debug.DrawRay(currentPosition, direction*raycastLength, Color.white, 0.01f);
            Vector3 vertex;
            
            if(raycastHit2D.collider != null){
                //Obstacle Hit
                if(raycastHit2D.collider.tag == "Player"){
                    Debug.Log("Player found!"); 
                }
                Debug.Log("Object hit!!");
                vertex = GetVectorFromAngle(angle) * raycastHit2D.distance;
            } else {
                //Did not hit obstacle
                Debug.Log("No object hit");
                vertex = GetVectorFromAngle(angle) * raycastLength;
            }

            vertices[vertexIndex] = vertex;

            if(i>0){
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex -1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            vertexIndex++;

            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        /*
        for(float deg=-coneAngle; deg < coneAngle; deg+=degIncrement){
            Vector2 direction = Quaternion.Euler(0,0,deg)*transform.up;
            direction = direction.normalized;
            float distance = CastRay(direction);
            Debug.DrawRay(transform.position, direction*distance, Color.white, 0.01f);
        }*/
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

    Vector3 GetVectorFromAngle(float angle){
        //float angleRad = angle * Mathf.Deg2Rad;
        //return new Vector3(Mathf.Sin(angleRad), Mathf.Cos(angleRad)).normalized;
        //return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));  
        return Quaternion.Euler(0,0,angle) * Vector3.up;
    }


}

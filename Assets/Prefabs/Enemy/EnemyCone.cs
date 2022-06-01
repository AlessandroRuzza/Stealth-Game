using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCone : MonoBehaviour
{
    [SerializeField] int raycastLength = 5;
    [SerializeField] float coneAngle = 15f;
    [SerializeField] int rayCount = 10;
    [SerializeField] bool debugLog;
    [SerializeField] bool debugRay;
    //float currentAngle = 0;
    float angleIncrease {
        get{ return 2*coneAngle / rayCount; }
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
            Vector2 rayDirection = Quaternion.Euler(0,0,angle) * transform.up;
            Vector3 vertexDirection = Quaternion.Euler(0,0,angle) * Vector3.up;
            
            RaycastHit2D raycastHit2D = Physics2D.Raycast(currentPosition, rayDirection, raycastLength);
            if(debugRay) Debug.DrawRay(currentPosition, rayDirection*raycastLength, Color.white, 0.01f);
            Vector3 vertex;
            
            if(raycastHit2D.collider != null){
                //Obstacle Hit
                if(raycastHit2D.collider.tag == "Player"){
                    if(debugLog) Debug.Log("Player found!"); 
                }
                if(debugLog) Debug.Log("Object hit!!");
                vertex = vertexDirection * raycastHit2D.distance;
            } else {
                //Did not hit obstacle
                if(debugLog) Debug.Log("No object hit");
                vertex = vertexDirection * raycastLength;
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
    }

}

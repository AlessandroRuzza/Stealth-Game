using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCone : MonoBehaviour
{
    [SerializeField] int raycastLength = 5;
    float degIncrement = 0.5f;
    [SerializeField] static float coneAngle = 15f;
    static int rayCount = 10;
    //float currentAngle = 0;
    float angleIncrease = coneAngle / rayCount;
    Vector3 origin = Vector3.zero;

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

        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[rayCount + 2];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int traingleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            //Vector2 direction = Quaternion.Euler(0,0,i)*transform.up;
            
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(coneAngle), raycastLength);
            Vector3 vertex;// = CastRay(direction);
            
            if(raycastHit2D.collider != null){
                //Obstacle Hit
                if(raycastHit2D.collider.tag == "Player"){
                Debug.Log("Player found!"); 
                }
                Debug.Log("Object hit!!");
                vertex = raycastHit2D.point;
            } else {
                //Did not hit obstacle
                Debug.Log("No object hit");
                vertex = origin + GetVectorFromAngle(coneAngle) * raycastLength;
            }


            vertices[vertexIndex] = vertex;

            if(i>0){
                triangles[traingleIndex] = 0;
                triangles[traingleIndex + 1] = vertexIndex -1;
                triangles[traingleIndex + 2] = vertexIndex;

                traingleIndex += 3;
            }
            vertexIndex++;

            coneAngle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        /*
        for(float deg=-coneAngle; deg < coneAngle; deg+=degIncrement){
            Vector2 direction = Quaternion.Euler(0,0,deg)*transform.up;
            direction = direction.normalized;
            float distance = CastRay(direction);
            //Debug.DrawRay(transform.position, direction*distance, Color.white, 0.01f);
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
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }


}

using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class EnemyCone : MonoBehaviour
{
    MeshFilter meshFilter;
    new Renderer renderer;
    [SerializeField] Material sourceMaterial;
    Material material;
    bool stopDrawing;

    [SerializeField] int raycastLength = 5;
    [SerializeField] float coneAngle = 15f;
    [SerializeField] int rayCount = 10;
    [SerializeField] bool debugLog;
    [SerializeField] bool debugRay;
    [SerializeField] float findRate;
    [SerializeField] float timeLimit;
    float timerToFound;
    bool doRaiseTimer;
    //float currentAngle = 0;
    float angleIncrease {
        get{ return 2*coneAngle / rayCount; }
    }

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        renderer = GetComponent<Renderer>();
        stopDrawing = false;
        timerToFound = 0;
        material = new Material(sourceMaterial);
        renderer.material = material;
        UpdateColor();
    }

    void Update()
    {
        if (stopDrawing) return;

        //Made test Mesh
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        Vector2 currentPosition = transform.position;
        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[rayCount + 2];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = Vector3.zero;

        int vertexIndex = 1;
        int triangleIndex = 0;
        float angle = coneAngle;
        doRaiseTimer = false;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector2 rayDirection = Quaternion.Euler(0,0,angle) * transform.up;
            Vector3 vertexDirection = Quaternion.Euler(0,0,angle) * Vector3.up;

            RaycastHit2D raycastHit2D = Physics2D.Raycast(currentPosition, rayDirection, raycastLength, LayerMask.GetMask("RaycastObstacle", "Player"));
            if(debugRay) Debug.DrawRay(currentPosition, rayDirection*raycastLength, Color.white, 0.01f);
            Vector3 vertex;
            
            if(raycastHit2D.collider != null)
            {
                //Obstacle Hit
                if (raycastHit2D.collider.TryGetComponent<Player>(out Player player))
                {   //if hit Player
                    if (debugLog) Debug.Log("Player found!");
                    if (timerToFound > timeLimit)
                    {
                        player.Spotted();
                        stopDrawing = true;
                    }
                    else
                        doRaiseTimer = true;
                    RaycastHit2D raycastHitPlayer2D = Physics2D.Raycast(currentPosition, rayDirection, raycastLength, LayerMask.GetMask("RaycastObstacle"));
                    if (raycastHitPlayer2D.collider != null)
                        vertex = vertexDirection * raycastHitPlayer2D.distance;
                    else
                        vertex = vertexDirection * raycastLength;
                }
                else
                    vertex = vertexDirection * raycastHit2D.distance;

                if (debugLog) Debug.Log("Object hit!");
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
        if (doRaiseTimer)
        {
            timerToFound += Time.deltaTime * findRate;
        }
        else
        {
            timerToFound -= Time.deltaTime * findRate;
            if (timerToFound < 0) timerToFound = 0;
        }
        UpdateColor();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    void UpdateColor()
    {
        float startGreenVal = 194 / 255f;
        float endGreenVal = 56 / 255f;

        Color newColor = material.color;
        newColor.g = Mathf.Lerp(startGreenVal, endGreenVal, timerToFound / timeLimit);
        material.color = newColor;
    }

}

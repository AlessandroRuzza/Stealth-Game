using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField] Camera playerCamera;
    
    Vector3 temp;
    [SerializeField] float sizeZoom;
    [SerializeField] float sizeNotZoom;

    // Start is called before the first frame update

    void Start()
    {
        playerCamera.orthographicSize = sizeZoom;
        //Switch for god view
        playerCamera.transform.position = transform.position + Vector3.back * 200;
        
    }

    

    // Update is called once per frame
    void Update()
    {
        temp = transform.position;
        //controlli

        temp.z = -200;


        playerCamera.transform.position = temp;
    }
}

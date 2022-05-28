using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 oldPosition;
    Rigidbody2D rigidbody2D;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        speed = 6;
    }

    void Update()
    {
        oldPosition = transform.position;
        int verticalMove=0;
        int horizontalMove=0;
        if(Input.GetKey(KeyCode.W)) verticalMove++;
        if(Input.GetKey(KeyCode.S)) verticalMove--;
        if(Input.GetKey(KeyCode.A)) horizontalMove--;
        if(Input.GetKey(KeyCode.D)) horizontalMove++;
        
        Vector2 force = new Vector2(horizontalMove, verticalMove);
        rigidbody2D.velocity = force.normalized*speed;
    }

    /*
    void OnCollisionEnter2D(Collision2D other) {        
        if(other.gameObject.tag == "Wall")
            transform.position = oldPosition;
    }
    */


}

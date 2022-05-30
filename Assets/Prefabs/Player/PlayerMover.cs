using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 startPosition;
    Vector2 force;
    bool endLevel;
    new Rigidbody2D rigidbody2D;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        startPosition = transform.position;
        endLevel = false;
    }

    void Update()
    {
        UpdateForce();
        if(Input.GetKeyDown(KeyCode.R) && endLevel) Reset();
    }

    void FixedUpdate() {    // it's better to do physics in "FixedUpdate"
        rigidbody2D.velocity = force.normalized*speed;
    }

    void UpdateForce(){
        int verticalMove=0;
        int horizontalMove=0;
        if(Input.GetKey(KeyCode.W)) verticalMove++;
        if(Input.GetKey(KeyCode.S)) verticalMove--;
        if(Input.GetKey(KeyCode.A)) horizontalMove--;
        if(Input.GetKey(KeyCode.D)) horizontalMove++;
        
        force = new Vector2(horizontalMove, verticalMove);
    }
    
    void OnTriggerEnter2D(Collider2D other) {       
        if(other.gameObject.tag != "End") return;
        
        Debug.Log("Arrived at End position!");
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        endLevel = true;
    }

    void Reset(){           // Cremascoli function
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigidbody2D.MovePosition(startPosition);
        endLevel = false;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float speed;
    Vector2 force;
    new Rigidbody2D rigidbody2D;
    Player player;
    public Animator animator;

    SpriteRenderer spriteRenderer;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        rigidbody2D.freezeRotation = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        UpdateForce();
        animator.SetFloat("movSpeed",rigidbody2D.velocity.magnitude);
        animator.SetBool("isDead",!player.isAlive);
        animator.updateMode=(player.isAlive) ? AnimatorUpdateMode.Normal : AnimatorUpdateMode.UnscaledTime;
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


        if(force.x!=0 && player.isAlive && Time.timeScale>0)
        {
            spriteRenderer.flipX = force.x<0;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other) {       
        if(other.gameObject.tag != "End") return;
        

        if (player.canFinishLevel)
        {
            Debug.Log("Arrived at End position!");
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            player.LevelComplete();
        }
        else
        {
            Debug.Log("Missing some coins...");
        }
    }

    //public void Reset(){           // Cremascoli function
    //    rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    //    rigidbody2D.MovePosition(startPosition);
    //    endLevel = false;
    //}


}

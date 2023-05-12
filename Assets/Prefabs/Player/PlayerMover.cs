using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ICommand
{
    void Execute();
}

public class MoveUpCommand : ICommand
{
    private PlayerMover _playerMover;

    public MoveUpCommand(PlayerMover playerMover)
    {
        _playerMover = playerMover;
    }

    public void Execute()
    {
        _playerMover.Force += Vector2.up;
    }
}

public class MoveDownCommand : ICommand
{
    private PlayerMover _playerMover;

    public MoveDownCommand(PlayerMover playerMover)
    {
        _playerMover = playerMover;
    }

    public void Execute()
    {
        _playerMover.Force += Vector2.down;
    }
}

public class MoveLeftCommand : ICommand
{
    private PlayerMover _playerMover;

    public MoveLeftCommand(PlayerMover playerMover)
    {
        _playerMover = playerMover;
    }

    public void Execute()
    {
        _playerMover.Force += Vector2.left;
    }
}

public class MoveRightCommand : ICommand
{
    private PlayerMover _playerMover;

    public MoveRightCommand(PlayerMover playerMover)
    {
        _playerMover = playerMover;
    }

    public void Execute()
    {
        _playerMover.Force += Vector2.right;
    }
}


public class CommandInvoker
{
    private Queue<ICommand> _commands = new Queue<ICommand>();

    public void AddCommand(ICommand command)
    {
        _commands.Enqueue(command);
    }

    public void ExecuteCommands()
    {
        while (_commands.Count > 0)
        {
            ICommand command = _commands.Dequeue();
            command.Execute();
        }
    }
}

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float speed;
    Vector2 force;
    new Rigidbody2D rigidbody2D;
    Player player;
    public Animator animator;

    SpriteRenderer spriteRenderer;
    private RewindManager rewindManager;
    public Vector2 Force
    {
        get { return force; }
        set { force = value; }
    }

    // Add an instance of the CommandInvoker
    private CommandInvoker commandInvoker;

    private void Awake() {
        rewindManager = GetComponent<RewindManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        rigidbody2D.freezeRotation = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        commandInvoker = new CommandInvoker();
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

    void UpdateForce()
    {
        if (!RewindManager.isRewinding)
        {
            force = Vector2.zero;

            if (Input.GetKey(KeyCode.W))
                commandInvoker.AddCommand(new MoveUpCommand(this));
            if (Input.GetKey(KeyCode.S))
                commandInvoker.AddCommand(new MoveDownCommand(this));
            if (Input.GetKey(KeyCode.A))
                commandInvoker.AddCommand(new MoveLeftCommand(this));
            if (Input.GetKey(KeyCode.D))
                commandInvoker.AddCommand(new MoveRightCommand(this));

            commandInvoker.ExecuteCommands();

            if (force.x != 0 && player.isAlive && Time.timeScale > 0)
            {
                spriteRenderer.flipX = force.x < 0;
            }
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

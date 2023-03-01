using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    public Animator anim { get; private set; } 
    public PlayerInputHandler inputHandler { get; private set; }    
    public Rigidbody2D rb { get; private set; }

    public Vector2 currentVelocity { get; private set; }   
    
    public int facingDirection { get; private set; }
    
    [SerializeField]
    private PlayerData playerData;

    private Vector2 workspace;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        moveState = new PlayerMoveState(this, StateMachine, playerData, "move");
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerInputHandler>();

        StateMachine.Initialize(idleState);
    }

    private void Update()
    {
        currentVelocity = rb.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, currentVelocity.y);
        rb.velocity = workspace;
        currentVelocity = workspace;
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180.0f, 0f);
    }
}

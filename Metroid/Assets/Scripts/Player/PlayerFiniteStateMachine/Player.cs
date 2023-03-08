using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerLandState landState { get; private set; }  
    public PlayerWallClimbState wallClimbState { get; private set; }
    public PlayerWallGrabState wallGrabState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }    
    public PlayerAttackState primaryAttackState { get; private set; }
    public PlayerAttackState secondaryAttackState { get; private set; } 

    public Animator anim { get; private set; } 
    public PlayerInputHandler inputHandler { get; private set; }    
    public Rigidbody2D rb { get; private set; }

    public Vector2 currentVelocity { get; private set; }   
    
    public int facingDirection { get; private set; }
    
    [SerializeField]
    private PlayerData playerData;

    private Vector2 workspace;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;

   
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        moveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        jumpState = new PlayerJumpState(this, StateMachine, playerData, "in air");
        inAirState = new PlayerInAirState(this, StateMachine, playerData, "in air");
        landState = new PlayerLandState(this, StateMachine, playerData, "land");
        wallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wall climb");
        wallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wall grab");
        wallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wall state");
        primaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "primary attack");
        secondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "secondary attack");
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

    public void SetVelocityY(float velocity)
    {
        workspace.Set(currentVelocity.x, velocity);
        rb.velocity = workspace;
        currentVelocity = workspace;
    }

    public bool CheckIfTouchingGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
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

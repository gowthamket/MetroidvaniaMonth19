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
    public PlayerLedgeClimbState ledgeClimbState { get; private set; }
    public PlayerWeaponPickupState weaponPickupState { get; private set; }

    public Core core { get; private set; }  
    public Animator anim { get; private set; } 
    public PlayerInputHandler inputHandler { get; private set; }    
    public Rigidbody2D rb { get; private set; }   

    public PlayerInventory inventory { get; private set; }  
    
    [SerializeField]
    private PlayerData playerData;

    private Vector2 workspace;

    public GameObject weaponMenu;
   
    private void Awake()
    {
        core = GetComponentInChildren<Core>();
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
        ledgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledge climb");
        weaponPickupState = new PlayerWeaponPickupState(this, StateMachine, playerData, "weapon pickup");
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerInputHandler>();
        inventory = GetComponent<PlayerInventory>();    
        weaponMenu.SetActive(false);

        primaryAttackState.SetWeapon(inventory.weapons[(int)CombatInputs.primary]);
        secondaryAttackState.SetWeapon(inventory.weapons[(int)CombatInputs.secondary]);

        StateMachine.Initialize(idleState);
    }

    private void Update()
    {
        //currentVelocity = rb.velocity;
        core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void PickupWeapon()
    {
        weaponMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DoneWithWeaponMenu()
    {
        weaponMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}

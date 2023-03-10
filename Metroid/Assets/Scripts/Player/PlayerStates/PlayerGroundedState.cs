using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;

    private bool jumpInput;
    private bool grabInput;
    private bool isGrounded;
    private bool isTouchingWall;

    protected Movement Movement { get => movement ??= core.GetCoreComponent(ref movement); }
    private Movement movement;

    protected CollisionSenses CollisionSenses { get => collisionSenses ??= core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Ground;
            isTouchingWall = CollisionSenses.Wall;
        }
           
    }

    public override void Enter()
    {
        base.Enter();
    }
        

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.inputHandler.NormInputX;
        jumpInput = player.inputHandler.jumpInput;
        grabInput = player.inputHandler.grabInput;  

        if (player.inputHandler.attackInputs[(int)(CombatInputs.primary)])
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        else if (player.inputHandler.attackInputs[(int)(CombatInputs.secondary)])
        {
            stateMachine.ChangeState(player.secondaryAttackState);
        }

        if (jumpInput && player.jumpState.CanJump())
        {
            player.inputHandler.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);
        }
        else if (!isGrounded)
        {
            player.jumpState.DecreaseAmountOfJumpsLeft();
            stateMachine.ChangeState(player.inAirState);
        }
        else if (isTouchingWall && grabInput)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;
    private Vector2 workspace;

    private bool isHanging;
    private bool isClimbing;

    private int xInput;
    private int yInput; 


    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.anim.SetBool("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHanging = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        core.movement.SetVelocityZero();
        player.transform.position = detectedPos;
        cornerPos = DetermineCornerPosition();

        startPos.Set(cornerPos.x - (core.movement.facingDirection * playerData.startOffset.x), cornerPos.y - playerData.startOffset.y);
        stopPos.Set(cornerPos.x + (core.movement.facingDirection * playerData.stopOffset.x), cornerPos.y + playerData.stopOffset.y);

        player.transform.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();

        isHanging = false;

        if (isClimbing)
        {
            player.transform.position = stopPos;
            isClimbing = false; 
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.idleState);
        }

        xInput = player.inputHandler.NormInputX;
        yInput = player.inputHandler.NormInputY;

        core.movement.SetVelocityZero();
        player.transform.position = startPos;

        if (xInput == core.movement.facingDirection && isHanging && !isClimbing)
        {
            isClimbing = true;
            player.anim.SetBool("climbLedge", true);
        }
        else if (yInput == -1 && isHanging && !isClimbing)
        {
            stateMachine.ChangeState(player.inAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(core.collisionSenses.WallCheck.position, Vector2.right * core.movement.facingDirection, core.collisionSenses.wallCheckDistance, core.collisionSenses.whatIsGround);
        float xDist = xHit.distance;
        workspace.Set(xDist * core.movement.facingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(core.collisionSenses.LedgeCheck.position + (Vector3)(workspace), Vector2.down, core.collisionSenses.LedgeCheck.position.y - core.collisionSenses.WallCheck.position.y, core.collisionSenses.whatIsGround);
        float yDist = yHit.distance;

        workspace.Set(core.collisionSenses.WallCheck.position.x + (xDist * core.movement.facingDirection), core.collisionSenses.LedgeCheck.position.y - yDist);
        return workspace;
    }

    public void SetDetected(Vector2 pos) => detectedPos = pos;
}

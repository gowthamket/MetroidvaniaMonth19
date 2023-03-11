using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPosition;
    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        holdPosition = player.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        HoldPosition();

        core.movement.SetVelocityX(0f);
        core.movement.SetVelocityY(0f);

        if (yInput > 0f)
        {
            stateMachine.ChangeState(player.wallClimbState);
        }
        else if (yInput < 0f || !grabInput)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }

    private void HoldPosition()
    {
        player.transform.position = holdPosition;

        core.movement.SetVelocityX(0f);
        core.movement.SetVelocityY(0f);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

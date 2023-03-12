using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    private Movement movement;
    private CollisionSenses collisionSenses;

    protected D_ChargeState stateData;

    protected bool isPlayerInMinAggroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;

    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isDetectingLedge = CollisionSenses.LedgeVerticalCheck;
        isDetectingWall = CollisionSenses.Wall;

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public virtual void Enter()
    {
        base.Enter();
        isChargeTimeOver = false;
        Movement.SetVelocityX(stateData.chargeSpeed * Movement.facingDirection);
    }

    public virtual void Exit()
    {
        base.Exit();
    }

    public virtual void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
        Movement.SetVelocityX(stateData.chargeSpeed * Movement.facingDirection);
    }

    public virtual void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isDetectingLedge = CollisionSenses.LedgeVerticalCheck;
        isDetectingWall = CollisionSenses.Wall;
    }
}

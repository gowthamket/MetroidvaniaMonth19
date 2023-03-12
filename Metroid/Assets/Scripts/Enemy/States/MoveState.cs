using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    private Movement movement;
    private CollisionSenses collisionSenses;

    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayerInMinAggroRange;

    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public virtual void Enter()
    {
        base.Enter();
        
    }

    public virtual void Exit()
    {
        base.Exit();    
    }

    public virtual void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(stateData.movementSpeed * Movement.facingDirection);
    }

    public virtual void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        isDetectingLedge = CollisionSenses.LedgeVerticalCheck;
        isDetectingWall = CollisionSenses.Wall;
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingLedge = CollisionSenses.LedgeVerticalCheck;
        isDetectingWall = CollisionSenses.Wall;
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }
}

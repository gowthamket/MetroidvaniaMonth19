using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected D_IdleState stateData;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAggroRange;

    protected float idleTime;

    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public virtual void Enter()
    {
        base.Enter();

        entity.SetVelocity(0);
        isIdleTimeOver = false;
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        SetRandomIdleTime();
    }

    public virtual void Exit()
    {
        base.Exit();

        if (flipAfterIdle)
        {
            entity.Flip();  
        }
    }

    public virtual void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;  
        }
    }

    public virtual void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}

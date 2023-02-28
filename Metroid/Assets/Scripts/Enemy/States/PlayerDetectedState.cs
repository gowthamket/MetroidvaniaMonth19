using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetected stateData;

    protected bool isPlayerInMinAggroRange;
    protected bool isPlayerInMaxAggroRange; 

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData; 
    }

    public virtual void Enter()
    {
        base.Enter();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();  
    }

    public virtual void Exit()
    {
        base.Exit();
    }

    public virtual void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public virtual void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

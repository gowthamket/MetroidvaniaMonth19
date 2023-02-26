using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;
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
    }

    public virtual void PhysicsUpdate()
    {
        base.PhysicsUpdate();   
    }
}

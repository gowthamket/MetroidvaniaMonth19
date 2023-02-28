using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;

    public E1_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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

        if (!isPlayerInMaxAggroRange)
        {
            enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public virtual void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

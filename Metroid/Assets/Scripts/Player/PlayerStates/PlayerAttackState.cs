using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapons weapon;

    private int xInput;

    private float velocityToSet;
    private bool setVelocity;

    private bool shouldCheckFlip;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        setVelocity = false;

        weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();

        weapon.ExitWeapon();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.inputHandler.NormInputX;

        Movement?.CheckIfShouldFlip(xInput);
        if (shouldCheckFlip)
        {
            Movement?.CheckIfShouldFlip(xInput);
        }

        if (setVelocity)
        {
            Movement?.SetVelocityX(velocityToSet * Movement.facingDirection);
        }
    }

    public void SetWeapon(Weapons weapon)
    {
        this.weapon = weapon;
        this.weapon.InitializeWeapon(this, core);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;   
    }

    public void SetPlayerVelocity(float velocity)
    {
        Movement?.SetVelocityX(velocity * Movement.facingDirection);

        velocityToSet = velocity;   
        setVelocity = true; 
    }

    public void SetFlipCheck(bool value)
    {
        shouldCheckFlip = value;
    }
}

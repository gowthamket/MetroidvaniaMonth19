using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] public SO_WeaponData weaponData;

    protected Animator baseAnimator;
    protected Animator weaponAnimator;

    [field: SerializeField]
    public Sprite PickupSprite { get; private set; }

    protected PlayerAttackState state;

    [field: SerializeField]
    public string WeaponName { get; private set; }

    [field: SerializeField, TextArea(3, 10)]
    public string WeaponDescription { get; private set; }

    protected Core core;

    protected int attackCounter;

    protected virtual void Awake()
    {
        baseAnimator = GetComponent<Animator>();    
        weaponAnimator = GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (attackCounter >= weaponData.movementSpeed.Length)
        {
            attackCounter = 0;
        }

        baseAnimator.SetBool("attack", true);
        weaponAnimator.SetBool("attack", true);

        baseAnimator.SetInteger("attackCounter", attackCounter);
        weaponAnimator.SetInteger("attackCounter", attackCounter);
    }

    public virtual void ExitWeapon()
    {
        gameObject.SetActive(false);

        attackCounter++;

        baseAnimator.SetBool("attack", false);
        weaponAnimator.SetBool("attack", false);
    }

    public virtual void AnimationFinishedTrigger()
    {
        state.AnimationFinishTrigger();

    }

    public virtual void AnimationStartMovementTrigger()
    {
        state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        state.SetPlayerVelocity(0f);
    }

    public virtual void AnimationTurnOffFlipTrigger()
    {
        state.SetFlipCheck(false);
    }

    public virtual void AnimationTurnOnFlipTrigger()
    {
        state.SetFlipCheck(true);
    }

    public virtual void AnimationActionTrigger()
    {

    }

    public void InitializeWeapon(PlayerAttackState state, Core core)
    {
        this.state = state;
        this.core = core;
    }
}

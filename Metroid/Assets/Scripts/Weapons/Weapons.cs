using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    protected Animator baseAnimator;
    protected Animator weaponAnimator;

    protected PlayerAttackState state;

    protected virtual void Start()
    {
        baseAnimator = GetComponent<Animator>();    
        weaponAnimator = GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        baseAnimator.SetBool("attack", true);
        weaponAnimator.SetBool("attack", true);
    }

    public virtual void ExitWeapon()
    {
        gameObject.SetActive(false);

        baseAnimator.SetBool("attack", false);
        weaponAnimator.SetBool("attack", false);
    }

    public virtual void AnimationFinishedTrigger()
    {
        state.AnimationFinishTrigger();

    }

    public void InitializeWeapon(PlayerAttackState state)
    {
        this.state = state;
    }
}

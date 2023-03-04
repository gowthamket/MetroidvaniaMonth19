using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    private Weapons weapon;


    private void Start()
    {
        weapon = GetComponentInParent<Weapons>();
    }

    private void AnimationFinishTrigger()
    {
        weapon.AnimationFinishedTrigger();
    }
}

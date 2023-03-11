using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitboxToWeapon : MonoBehaviour
{
    private AggressiveWeapon weapon;

    private void Awake()
    {
        weapon = GetComponentInParent<AggressiveWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        weapon.AddToDetected(collider2D);
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        weapon.RemoveFromDetected(collider2D);
    }
}

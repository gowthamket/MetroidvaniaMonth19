using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponChangedChannel", menuName = "Event Channels/Weapon Changes")]
public class WeaponChangedEventChannel : EventChannelsSO<WeaponChangedEventArgs> { }

public class WeaponChangedEventArgs : EventArgs
{
    public Weapons WeaponData;
    public CombatInputs WeaponInput;

    public WeaponChangedEventArgs(Weapons weaponData, CombatInputs weaponInput)
    {
        WeaponData = weaponData;
        WeaponInput = weaponInput;
    }
}

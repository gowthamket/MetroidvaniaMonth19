using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponPickupChannel", menuName = "Event Channels/Weapon Pickup")]
public class WeaponPickupEventChannel : EventChannelsSO<WeaponPickupEventArgs>
{

}

public class WeaponPickupEventArgs : EventArgs
{
    public Weapons NewWeaponData;
    public Weapons PrimaryWeaponData;
    public Weapons SecondaryWeaponData;

    public WeaponPickupEventArgs(Weapons newWeaponData, Weapons primaryWeaponData, Weapons secondaryWeaponData)
    {
        NewWeaponData = newWeaponData;
        PrimaryWeaponData = primaryWeaponData;
        SecondaryWeaponData = secondaryWeaponData;
    }
}

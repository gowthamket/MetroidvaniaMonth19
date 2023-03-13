using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopulateWeaponInfo : MonoBehaviour
{
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private TMP_Text weaponDescription;

    public void SetWeaponInfo(Weapons data)
    {
        weaponIcon.sprite = data.PickupSprite;
        weaponName.text = data.WeaponName;
        weaponDescription.text = data.WeaponDescription;
    }
}

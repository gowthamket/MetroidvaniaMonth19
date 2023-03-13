using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapWeaponUI : MonoBehaviour
{
    private CanvasGroup CG;

    [SerializeField] private WeaponPickupEventChannel channel;
    [SerializeField] private TriggerEventChannel TriggerDeselectUIChannel;

    private SwapWeaponChoiceUI[] weaponChoices;

    [SerializeField] private PopulateWeaponInfo primarySlot;
    [SerializeField] private PopulateWeaponInfo secondarySlot;
    [SerializeField] private PopulateWeaponInfo newWeaponSlot;


    private PlayerInventory inventory;
    private Weapons newWeaponData;

    private void Awake()
    {
        CG = GetComponent<CanvasGroup>();

        SetUIActive(false);

        weaponChoices = GetComponentsInChildren<SwapWeaponChoiceUI>();

        foreach (var choice in weaponChoices)
        {
            choice.OnChoiceSelected += HandleChoiceSelectedEvent;
        }

        channel.OnEvent += HandlePickupEvent;
    }

    private void SetUIActive(bool value)
    {
        CG.alpha = value ? 1f : 0f;
        CG.interactable = value;
    }

    private void HandlePickupEvent(object sender, WeaponPickupEventArgs context)
    {
        SetUIActive(true);

        primarySlot.SetWeaponInfo(context.PrimaryWeaponData);
        secondarySlot.SetWeaponInfo(context.SecondaryWeaponData);
        newWeaponSlot.SetWeaponInfo(context.NewWeaponData);

        newWeaponData = context.NewWeaponData;

        if (sender is PlayerInventory inv)
        {
            inventory = inv;
        }
    }

    private void HandleChoiceSelectedEvent(CombatInputs inputChoice)
    {
        SetUIActive(false);

        TriggerDeselectUIChannel.RaiseEvent(this, EventArgs.Empty);

        inventory.SetWeapon(newWeaponData, inputChoice);
    }

    private void OnDisable()
    {
        channel.OnEvent -= HandlePickupEvent;

        foreach (var choice in weaponChoices)
        {
            choice.OnChoiceSelected -= HandleChoiceSelectedEvent;
        }
    }
}

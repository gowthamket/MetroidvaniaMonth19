using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapWeaponChoiceUI : MonoBehaviour
{
    [SerializeField] private CombatInputs input;

    private Button button;

    public event Action<CombatInputs> OnChoiceSelected;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable() => button.onClick.AddListener(HandleButtonClick);

    private void OnDisable() => button.onClick.RemoveListener(HandleButtonClick);

    private void HandleButtonClick()
    {
        OnChoiceSelected?.Invoke(input);
    }

}

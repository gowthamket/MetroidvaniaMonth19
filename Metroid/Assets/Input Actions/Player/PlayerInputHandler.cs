using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    public Vector2 rawMovementInput { get; private set; }

    public int NormInputX { get; private set; } 
    public int NormInputY { get; private set; }
    public bool jumpInput { get; private set; }
    public bool jumpInputStop { get; private set; } 
    public bool grabInput { get; private set; }


    public bool weaponPickupInput { get; private set; }
    public int weaponPickupCount { get; private set; }

    public bool[] attackInputs { get; private set; }

    [SerializeField]
    public float inputHoldTime = 0.2f;

    private float jumpInputStartTime;

    public void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        attackInputs = new bool[count];
        cam = Camera.main;

    }

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attackInputs[(int)(CombatInputs.primary)] = true;
        }

        if (context.canceled)
        {
            attackInputs[(int)(CombatInputs.primary)] = false;
        }
    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attackInputs[(int)(CombatInputs.secondary)] = true;
        }

        if (context.canceled)
        {
            attackInputs[(int)(CombatInputs.secondary)] = false;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();
        NormInputX = Mathf.RoundToInt(rawMovementInput.x);
        NormInputY = Mathf.RoundToInt(rawMovementInput.y);

        
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpInput = true;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            jumpInputStop = true;   
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();

        if (context.started)
        {
            grabInput = true;
        }

        if (context.canceled)
        {
            grabInput = false;
        }
    }

    public void UseJumpInput()
    {
        jumpInput = false;
    }

    public void OnPickupInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (weaponPickupCount == 0)
            {
                weaponPickupInput = true;
                weaponPickupCount = 1;  
            }
            else if (weaponPickupCount == 1)
            {
                weaponPickupInput = false;
            }
        }
    }

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            jumpInput = false;
        }
    }
}

public enum CombatInputs
{
    primary,
    secondary,
}

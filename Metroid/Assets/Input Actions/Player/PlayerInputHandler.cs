using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 rawMovementInput { get; private set; }

    public int NormInputX { get; private set; } 
    public int NormInputY { get; private set; }
    public bool jumpInput { get; private set; }
    public bool jumpInputStop { get; private set; } 
    public bool grabInput { get; private set; }

    [SerializeField]
    public float inputHoldTime = 0.2f;

    private float jumpInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();
        if (Mathf.Abs(rawMovementInput.x) > 0.5f)
        {
            NormInputX = (int)(rawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            NormInputX = 0;
        }

        if (Mathf.Abs(rawMovementInput.y) > 0.5f)
        {
            NormInputY = (int)(rawMovementInput * Vector2.up).normalized.y;
        }
        else
        {
            NormInputY = 0;
        }
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

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            jumpInput = false;
        }
    }
}

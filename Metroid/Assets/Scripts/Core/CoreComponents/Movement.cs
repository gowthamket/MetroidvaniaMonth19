using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    private Vector2 workspace;
    public Vector2 currentVelocity { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public int facingDirection { get; private set; }

    public bool canSetVelocity { get; set; }

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponentInParent<Rigidbody2D>();

        facingDirection = 1;
        canSetVelocity = true;

    }

    public void LogicUpdate()
    {
        currentVelocity = rb.velocity;
    }

    public void SetVelocityZero()
    {
        workspace = Vector2.zero;
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = workspace;
        currentVelocity = workspace;
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, currentVelocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(currentVelocity.x, velocity);
        SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {
        if (canSetVelocity)
        {
            rb.velocity = workspace;
            currentVelocity = workspace;
        }
        
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        facingDirection *= -1;
        rb.transform.Rotate(0f, 180.0f, 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    public Transform GroundCheck { get => groundCheck; private set => groundCheck = value; }
    public Transform WallCheck { get => groundCheck; private set => groundCheck = value; }
    public Transform LedgeCheck { get => groundCheck; private set => groundCheck = value; }
    public Transform CeilingCheck { get => groundCheck; private set => groundCheck = value; }

    [SerializeField]
    public Transform groundCheck;
    [SerializeField]
    public Transform wallCheck;
    [SerializeField]
    public Transform ledgeCheck;
    [SerializeField]
    public Transform ceilingCheck;

    [SerializeField]
    float groundCheckRadius;
    [SerializeField]
    public LayerMask whatIsGround;
    [SerializeField]
    public float wallCheckDistance;

    public bool Ceiling
    {
        get => Physics2D.OverlapCircle(ceilingCheck.position, groundCheckRadius);
    }

    public bool Ground
    {
        get => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    public bool Wall
    {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * core.movement.facingDirection, wallCheckDistance, whatIsGround);
    }

    public bool Ledge
    {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * core.movement.facingDirection, wallCheckDistance, whatIsGround);
    }
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
}

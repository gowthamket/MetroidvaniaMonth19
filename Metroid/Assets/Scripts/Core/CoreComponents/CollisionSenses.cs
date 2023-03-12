using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    private Movement movement;
    
    public Transform GroundCheck { 
        get
        {
            if (groundCheck)
            {
                return groundCheck;
            }
            Debug.LogError("No Ground Check on " + transform.parent.name);
            return null;
        }
        private set => groundCheck = value; }
    public Transform WallCheck {
        get
        {
            if (wallCheck)
            {
                return wallCheck;
            }
            Debug.LogError("No Wall Check on " + transform.parent.name);
            return null;
        }
        private set => wallCheck = value; }
    public Transform LedgeHorizontalCheck {
        get
        {
            if (ledgeHorizontalCheck)
            {
                return ledgeHorizontalCheck;
            }
            Debug.LogError("No Ledge Horizontal Check on " + transform.parent.name);
            return null;
        }
        private set => ledgeHorizontalCheck = value;}
    public Transform LedgeVerticalCheck {
        get
        {
            if (ledgeVerticalCheck)
            {
                return ledgeVerticalCheck;
            }
            Debug.LogError("No Ledge Vertical Check on " + transform.parent.name);
            return null;
        }
        private set => ledgeVerticalCheck = value; }
    
    public Transform CeilingCheck {
        get
        {
            if (ceilingCheck)
            {
                return ceilingCheck;
            }
            Debug.LogError("No Ceiling Check on " + transform.parent.name);
            return null;
        }
        private set => ceilingCheck = value; }

    [SerializeField]
    public Transform groundCheck;
    [SerializeField]
    public Transform wallCheck;
    [SerializeField]
    public Transform ceilingCheck;
    [SerializeField]
    public Transform ledgeHorizontalCheck;
    [SerializeField]
    public Transform ledgeVerticalCheck;

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
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * Movement.facingDirection, wallCheckDistance, whatIsGround);
    }

    public bool LedgeHorizontal
    {
        get => Physics2D.Raycast(ledgeHorizontalCheck.position, Vector2.right * Movement.facingDirection, wallCheckDistance, whatIsGround);
    }

    public bool LedgeVertical
    {
        get => Physics2D.Raycast(ledgeVerticalCheck.position, Vector2.right * Movement.facingDirection, wallCheckDistance, whatIsGround);
    }
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
}

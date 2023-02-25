using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Walking,
        Knockback,
        Dead
    };

    private State currentState;

    [SerializeField]
    private float
        groundCheckDistance,
        wallCheckDistance,
        movementSpeed,
        maxHealth,
        knockbackDuration,
        lastTouchDamageTime,
        touchDamageCooldown,
        touchDamage,
        touchDamageHeight,
        touchDamageWidth;
    [SerializeField]
    private Transform
        groundCheck,
        wallCheck,
        touchDamageCheck;
    [SerializeField]
    private LayerMask 
        whatIsPlayer,
        whatIsGround;
    [SerializeField]
    private Vector2 knockBackSpeed;
    [SerializeField]
    private GameObject
        hitParticle,
        deathChunkParticle,
        deathBloodParticle;

    private float
        currentHealth,
        knockBackStartTime;

    private float[] attackDetails = new float[2];

    private int
        facingDirection,
        damageDirection;

    private Vector2 
        touchDamageBotLeft,
        touchDamageTopRight,
        movement;

    private bool
        groundDetected,
        wallDetected;

    private GameObject alive;
    private Rigidbody2D aliveRb;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        facingDirection = 1;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.down, wallCheckDistance, whatIsGround);

        CheckTouchDamage();

        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
            aliveRb.velocity = movement;
        }
    }

    private void ExitMovingState()
    {

    }

    private void EnterKnockbackState()
    {
        knockBackStartTime = Time.time;
        movement.Set(knockBackSpeed.x * damageDirection, knockBackSpeed.y);
        aliveRb.velocity = movement;
    }

    private void UpdateKnockbackState()
    {
        if (Time.time >= knockBackStartTime + knockbackDuration)
        {
            SwitchState(State.Walking);
        }
    }

    private void ExitKnockbackState()
    {

    }

    private void EnterDeadState()
    {
        Instantiate(deathChunkParticle, alive.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];

        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360f)));

        if (attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        if (currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if (currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void CheckTouchDamage()
    {
        if (Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if (hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = alive.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walking:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Walking:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x, wallCheck.position.y));
    }
}
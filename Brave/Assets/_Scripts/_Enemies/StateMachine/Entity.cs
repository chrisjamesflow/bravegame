using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private Movement Movement { get => movement ?? Core.GetCoreComponent(ref movement); }
    private Movement movement;

    //private Stats Stats { get => stats ?? Core.GetCoreComponent(ref stats); }
    //private Stats stats;

    public FiniteStateMachine stateMachine;

    public D_Entity entityData;

    public Animator anim { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }
    public Core Core { get; private set; }
    public AudioManager Audio { get; private set; }

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform jumpWallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform jumpUpCheck;
    [SerializeField]
    private Transform jumpDownCheck;
    [SerializeField]
    private Transform playerCheckBelow;
    [SerializeField]
    private Transform playerCheckLow;
    [SerializeField]
    private Transform playerCheckMid;
    [SerializeField]
    private Transform playerCheckHigh;

    //private float currentHealth;
    //private float currentStunResistance;
    //private float lastDamageTime;

    private Vector2 velocityWorkspace;

    //protected bool isStunned;
    protected bool isDead;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        //currentHealth = entityData.maxHealth;
        //currentStunResistance = entityData.stunResistance;

        Audio = GetComponent<AudioManager>();
        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStateMachine>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();

        //anim.SetFloat("yVelocity", Core.Movement.RB.velocity.y);

        /*if(Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }*/
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual bool CheckWallForJump()
    {
        return Physics2D.Raycast(jumpWallCheck.position, transform.right, entityData.jumpWallCheckDistance, entityData.whatIsGround);
    }
    public virtual bool CheckJumpUp()
    {
        return Physics2D.Raycast(jumpUpCheck.position, transform.right, entityData.jumpUpCheckDistance, entityData.whatIsGround);
    }
    public virtual bool CheckJumpDown()
    {
        return Physics2D.Raycast(jumpDownCheck.position, Vector2.down, entityData.jumpDownCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckPlayerInLowAgroRange()
    {
        return Physics2D.Raycast(playerCheckLow.position, transform.right, entityData.lowAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInLowMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheckLow.position, transform.right, entityData.lowMaxAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInMidAgroRange()
    {
        return Physics2D.Raycast(playerCheckMid.position, transform.right, entityData.midAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInMidMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheckMid.position, transform.right, entityData.midMaxAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInHighAgroRange()
    {
        return Physics2D.Raycast(playerCheckHigh.position, transform.right, entityData.highAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInHighMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheckHigh.position, transform.right, entityData.highMaxAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerBehindAgroRangeBelow()
    {
        return Physics2D.Raycast(playerCheckBelow.position, -transform.right, entityData.behindAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerBehindAgroRangeLow()
    {
        return Physics2D.Raycast(playerCheckLow.position, -transform.right, entityData.behindAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerBehindAgroRangeMid()
    {
        return Physics2D.Raycast(playerCheckMid.position, -transform.right, entityData.behindAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerBehindAgroRangeHigh()
    {
        return Physics2D.Raycast(playerCheckHigh.position, -transform.right, entityData.behindAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerBehindMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheckLow.position, -transform.right, entityData.behindMaxAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeActionLow()
    {
        return Physics2D.Raycast(playerCheckLow.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInCloseRangeActionMid()
    {
        return Physics2D.Raycast(playerCheckMid.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInCloseRangeActionHigh()
    {
        return Physics2D.Raycast(playerCheckHigh.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerBehindCloseRangeActionLow()
    {
        return Physics2D.Raycast(playerCheckLow.position, -transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerBehindCloseRangeActionMid()
    {
        return Physics2D.Raycast(playerCheckMid.position, -transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(Movement.RB.velocity.x, velocity);
        Movement.RB.velocity = velocityWorkspace;
    }

    /*public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }*/

    public virtual void OnDrawGizmos()
    {
        if(Core != null)
        {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.wallCheckDistance));
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
            Gizmos.DrawLine(jumpUpCheck.position, jumpUpCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.jumpUpCheckDistance));
            Gizmos.DrawLine(jumpWallCheck.position, jumpWallCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.jumpWallCheckDistance));
            Gizmos.DrawLine(jumpDownCheck.position, jumpDownCheck.position + (Vector3)(Vector2.down * entityData.jumpDownCheckDistance));

            Gizmos.DrawWireSphere(playerCheckLow.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheckLow.position + (Vector3)(Vector2.right * entityData.lowAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheckLow.position + (Vector3)(Vector2.right * entityData.lowMaxAgroDistance), 0.2f);
        }
    }
}

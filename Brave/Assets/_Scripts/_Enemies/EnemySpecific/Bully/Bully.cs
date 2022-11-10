using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bully : Entity
{
    public Bully_IdleState idleState { get; private set; }
    public Bully_MoveState moveState { get; private set; }
    public Bully_PlayerDetectedState playerDetectedState { get; private set; }
    public Bully_ChargeState chargeState { get; private set; }
    public Bully_LookForPlayerState lookForPlayerState { get; private set; }
    public Bully_JumpUpState jumpUpState { get; private set; }
    public Bully_JumpDownState jumpDownState { get; private set; }
    public Bully_MeleeAttackState meleeAttackState { get; private set; }

    [SerializeField]
    private Transform meleeAttackPosition;

    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_JumpDownState jumpDownStateData;
    [SerializeField]
    private D_JumpUpState jumpUpStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;

    public override void Awake()
    {
        base.Awake();

        moveState = new Bully_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Bully_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Bully_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new Bully_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new Bully_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        jumpUpState = new Bully_JumpUpState(this, stateMachine, "jumpUp", jumpUpStateData, this);
        jumpDownState = new Bully_JumpDownState(this, stateMachine, "jumpDown", jumpDownStateData, this);
        meleeAttackState = new Bully_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
    }

    private void Start()
    {
        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}

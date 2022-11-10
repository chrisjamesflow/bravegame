using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Entity
{
    public Wolf_IdleState idleState { get; private set; }
    public Wolf_MoveState moveState { get; private set; }
    public Wolf_PlayerDetectedState playerDetectedState { get; private set; }
    public Wolf_ChargeState chargeState { get; private set; }
    public Wolf_LookForPlayerState lookForPlayerState { get; private set; }
    public Wolf_JumpUpState jumpUpState { get; private set; }
    public Wolf_JumpDownState jumpDownState { get; private set; }
    public Wolf_MeleeAttackState meleeAttackState { get; private set; }

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

        moveState = new Wolf_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Wolf_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Wolf_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new Wolf_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new Wolf_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        jumpUpState = new Wolf_JumpUpState(this, stateMachine, "jumpUp", jumpUpStateData, this);
        jumpDownState = new Wolf_JumpDownState(this, stateMachine, "jumpDown", jumpDownStateData, this);
        meleeAttackState = new Wolf_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
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

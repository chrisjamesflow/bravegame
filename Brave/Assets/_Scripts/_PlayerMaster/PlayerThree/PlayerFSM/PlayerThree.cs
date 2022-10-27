using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThree : MonoBehaviour
{
    #region State Variables
    public PlayerThreeStateMachine StateMachine { get; private set; }

    public PlayerThreeIdleState IdleState { get; private set; }
    public PlayerThreeMoveState MoveState { get; private set; }
    public PlayerThreeJumpState JumpState { get; private set; }
    public PlayerThreeInAirState InAirState { get; private set; }
    public PlayerThreeLandState LandState { get; private set; }
    public PlayerThreeWallSlideState WallSlideState { get; private set; }
    public PlayerThreeWallGrabState WallGrabState { get; private set; }
    public PlayerThreeWallClimbState WallClimbState { get; private set; }
    public PlayerThreeWallJumpState WallJumpState { get; private set; }
    public PlayerThreeLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerThreeCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerThreeCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerThreeAttackState PrimaryAttackState { get; private set; }
    public PlayerThreeAttackState SecondaryAttackState { get; private set; }
    public PlayerThreeAttackState TertiaryAttackState { get; private set; }


    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    public AudioManager Audio { get; private set; }
    public PlayerThreeInventory Inventory { get; private set; }
    #endregion

    #region Other Variables
    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerThreeStateMachine();

        IdleState = new PlayerThreeIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerThreeMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerThreeJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerThreeInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerThreeLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerThreeWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerThreeWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerThreeWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerThreeWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerThreeLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        CrouchIdleState = new PlayerThreeCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerThreeCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        PrimaryAttackState = new PlayerThreeAttackState(this, StateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerThreeAttackState(this, StateMachine, playerData, "attack");
        TertiaryAttackState = new PlayerThreeAttackState(this, StateMachine, playerData, "attack");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        Audio = GetComponent<AudioManager>();
        Inventory = GetComponent<PlayerThreeInventory>();

        PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        //SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.secondary]);

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Other Functions

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workspace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workspace;
        MovementCollider.offset = center;
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    #endregion
}

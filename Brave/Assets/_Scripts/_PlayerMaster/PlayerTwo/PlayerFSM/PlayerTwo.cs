using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo : MonoBehaviour
{
    #region State Variables
    public PlayerTwoStateMachine StateMachine { get; private set; }

    public PlayerTwoIdleState IdleState { get; private set; }
    public PlayerTwoMoveState MoveState { get; private set; }
    public PlayerTwoJumpState JumpState { get; private set; }
    public PlayerTwoInAirState InAirState { get; private set; }
    public PlayerTwoLandState LandState { get; private set; }
    public PlayerTwoWallSlideState WallSlideState { get; private set; }
    public PlayerTwoWallGrabState WallGrabState { get; private set; }
    public PlayerTwoWallClimbState WallClimbState { get; private set; }
    public PlayerTwoWallJumpState WallJumpState { get; private set; }
    public PlayerTwoLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerTwoCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerTwoCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerTwoAttackState PrimaryAttackState { get; private set; }
    public PlayerTwoAttackState SecondaryAttackState { get; private set; }
    public PlayerTwoAttackState TertiaryAttackState { get; private set; }


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
    public PlayerTwoInventory Inventory { get; private set; }
    #endregion

    #region Other Variables
    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerTwoStateMachine();

        IdleState = new PlayerTwoIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerTwoMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerTwoJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerTwoInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerTwoLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerTwoWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerTwoWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerTwoWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerTwoWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerTwoLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        CrouchIdleState = new PlayerTwoCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerTwoCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        PrimaryAttackState = new PlayerTwoAttackState(this, StateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerTwoAttackState(this, StateMachine, playerData, "attack");
        TertiaryAttackState = new PlayerTwoAttackState(this, StateMachine, playerData, "attack");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        Audio = GetComponent<AudioManager>();
        Inventory = GetComponent<PlayerTwoInventory>();

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

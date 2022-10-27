using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour
{
    #region State Variables
    public PlayerOneStateMachine StateMachine { get; private set; }

    public PlayerOneIdleState IdleState { get; private set; }
    public PlayerOneMoveState MoveState { get; private set; }
    public PlayerOneJumpState JumpState { get; private set; }
    public PlayerOneInAirState InAirState { get; private set; }
    public PlayerOneLandState LandState { get; private set; }
    public PlayerOneWallSlideState WallSlideState { get; private set; }
    public PlayerOneWallGrabState WallGrabState { get; private set; }
    public PlayerOneWallClimbState WallClimbState { get; private set; }
    public PlayerOneWallJumpState WallJumpState { get; private set; }
    public PlayerOneLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerOneCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerOneCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerOneGlideState GlideState { get; private set; }
    public PlayerOneAttackState PrimaryAttackState { get; private set; }
    public PlayerOneAttackState SecondaryAttackState { get; private set; }
    public PlayerOneAttackState TertiaryAttackState { get; private set; }


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
    public PlayerOneInventory Inventory { get; private set; }
    #endregion

    #region Other Variables
    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerOneStateMachine();

        IdleState = new PlayerOneIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerOneMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerOneJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerOneInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerOneLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerOneWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerOneWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerOneWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerOneWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerOneLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        CrouchIdleState = new PlayerOneCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerOneCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        GlideState = new PlayerOneGlideState(this, StateMachine, playerData, "glide");
        PrimaryAttackState = new PlayerOneAttackState(this, StateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerOneAttackState(this, StateMachine, playerData, "attack");
        TertiaryAttackState = new PlayerOneAttackState(this, StateMachine, playerData, "attack");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        Audio = GetComponent<AudioManager>();
        Inventory = GetComponent<PlayerOneInventory>();

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

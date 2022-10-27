using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFive : MonoBehaviour
{
    #region State Variables
    public PlayerFiveStateMachine StateMachine { get; private set; }

    public PlayerFiveIdleState IdleState { get; private set; }
    public PlayerFiveMoveState MoveState { get; private set; }
    public PlayerFiveJumpState JumpState { get; private set; }
    public PlayerFiveInAirState InAirState { get; private set; }
    public PlayerFiveLandState LandState { get; private set; }
    public PlayerFiveAttackState PrimaryAttackState { get; private set; }
    public PlayerFiveAttackState SecondaryAttackState { get; private set; }
    public PlayerFiveAttackState TertiaryAttackState { get; private set; }


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
    public PlayerFiveInventory Inventory { get; private set; }
    #endregion

    #region Other Variables
    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerFiveStateMachine();

        IdleState = new PlayerFiveIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerFiveMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerFiveJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerFiveInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerFiveLandState(this, StateMachine, playerData, "land");
        PrimaryAttackState = new PlayerFiveAttackState(this, StateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerFiveAttackState(this, StateMachine, playerData, "attack");
        TertiaryAttackState = new PlayerFiveAttackState(this, StateMachine, playerData, "attack");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        Audio = GetComponent<AudioManager>();
        Inventory = GetComponent<PlayerFiveInventory>();

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

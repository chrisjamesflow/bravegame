using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThree : MonoBehaviour
{
    [SerializeField] protected SO_WeaponData weaponData;

    protected Animator baseAnimator;

    protected PlayerThreeAttackState state;

    protected Core core;

    protected int attackCounter;

    protected virtual void Awake()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {

        if(attackCounter >= weaponData.amountOfAttacks)
        {
            attackCounter = 0;
        }
        gameObject.SetActive(true);

        baseAnimator.SetBool("attack", true);
    }

    public virtual void ExitWeapon()
    {
        gameObject.SetActive(false);

        baseAnimator.SetBool("attack", false);
    }

    #region Animation Triggers

    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        state.SetPlayerVelocity(weaponData.movementSpeed[3]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        state.SetPlayerVelocity(0f);
    }

    public virtual void AnimationActionTrigger()
    {

    }

    #endregion

    public void InitializeWeapon(PlayerThreeAttackState state, Core core)
    {
        this.state = state;
        this.core = core;
    }
}

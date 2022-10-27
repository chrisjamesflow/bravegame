using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeaponThree : MonoBehaviour
{
    private WeaponThree weapon;

    private void Start()
    {
        weapon = GetComponentInParent<WeaponThree>();
    }

    private void AnimationFinishTrigger()
    {
        weapon.AnimationFinishTrigger();
    }

    private void AnimationStartMovementTrigger()
    {
        weapon.AnimationStartMovementTrigger();
    }

    private void AnimationStopMovementTrigger()
    {
        weapon.AnimationStopMovementTrigger();
    }

    private void AnimationActionTrigger()
    {
        weapon.AnimationActionTrigger();
    }
}

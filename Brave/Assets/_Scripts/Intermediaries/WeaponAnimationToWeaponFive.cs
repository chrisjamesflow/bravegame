using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeaponFive : MonoBehaviour
{
    private WeaponFive weapon;

    private void Start()
    {
        weapon = GetComponentInParent<WeaponFive>();
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

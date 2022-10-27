using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeaponFour : MonoBehaviour
{
    private WeaponFour weapon;

    private void Start()
    {
        weapon = GetComponentInParent<WeaponFour>();
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

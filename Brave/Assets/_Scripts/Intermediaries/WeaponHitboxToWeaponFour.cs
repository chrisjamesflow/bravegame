using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitboxToWeaponFour : MonoBehaviour
{
    private AggressiveWeaponFour weapon;

    private void Awake()
    {
        weapon = GetComponentInParent<AggressiveWeaponFour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        weapon.AddToDetected(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        weapon.RemoveFromDetected(collision);
    }
}

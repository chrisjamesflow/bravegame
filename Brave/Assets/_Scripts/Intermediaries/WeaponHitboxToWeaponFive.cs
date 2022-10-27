using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitboxToWeaponFive : MonoBehaviour
{
    private AggressiveWeaponFive weapon;

    private void Awake()
    {
        weapon = GetComponentInParent<AggressiveWeaponFive>();
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

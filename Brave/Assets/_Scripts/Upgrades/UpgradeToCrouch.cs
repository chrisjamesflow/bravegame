using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeToCrouch : MonoBehaviour
{
    //public ParticleSystem upgradeEffect;
    //public Transform playerPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Success!");
            PlayerLedgeClimbState.crouchAbility = true;
            PlayerGroundedState.crouchAbility = true;
            //Instantiate(upgradeEffect, playerPosition);
            Destroy(gameObject);
        }
    }
}

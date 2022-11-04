using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeToDoubleJump : MonoBehaviour
{
    //public ParticleSystem upgradeEffect;
    //public Transform playerPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Success!");
            PlayerJumpState.jumpAmount = 2;
            //Instantiate(upgradeEffect, playerPosition);
            Destroy(gameObject);
        }
    }
}

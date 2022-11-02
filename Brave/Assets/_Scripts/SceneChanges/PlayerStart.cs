using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    public GameObject startPlayer;
    private GameObject existingPlayer;
    public Transform currentPosition;

    void Start()
    {
        existingPlayer = GameObject.FindWithTag("Player");

        if (existingPlayer == null)
        {
            Instantiate(startPlayer, currentPosition);
        }
    }
}

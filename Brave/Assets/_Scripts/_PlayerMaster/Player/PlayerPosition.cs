using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public SO_PlayerVectorValue StartingPosition;

    void Start()
    {
        transform.position = StartingPosition.initialPlayerPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollowTarget : MonoBehaviour
{
    public GameObject player;
    public Transform followTarget;
    private CinemachineVirtualCamera vcam;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                followTarget = player.transform;
                vcam.Follow = followTarget;
            }
        }
    }
}

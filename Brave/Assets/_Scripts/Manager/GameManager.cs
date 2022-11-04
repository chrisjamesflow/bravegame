using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private CinemachineVirtualCamera CVC;
    private AudioManager enemyAudio;

    [SerializeField]
    private Transform respawnPoint;

    [SerializeField]
    private float respawnTime;

    [SerializeField]
    public GameObject player;

    private float respawnTimeStart;

    private bool respawn;

    [SerializeField]
    private GameObject[] enemyAwake;

    [SerializeField]
    private GameObject[] enemyAsleep;

    [SerializeField]
    private float sleepTime = 2f;

    private float sleepStartTime;

    private bool asleep;
    
    private void Start()
    {
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();

        enemyAudio = GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioManager>();
    }

    private void Update()
    {
        CheckRespawn();
        CheckSleepTime();
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }
    
    public void WakeUp()
    {
        sleepStartTime = Time.time;
        asleep = true;
    }

    private void CheckRespawn()
    {
        if(Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            var playerTemp = Instantiate(player, respawnPoint);
            CVC.m_Follow = playerTemp.transform;
            respawn = false;
        }
    }

    private void CheckSleepTime()
    {
        if(Time.time >= sleepStartTime + sleepTime && asleep)
        {
            enemyAwake[0].transform.gameObject.SetActive(true);
            enemyAudio.Stop("Charge");
            enemyAudio.Stop("Move");
            enemyAudio.Stop("Idle");
            enemyAudio.Stop("Attack");
            enemyAudio.Stop("Sniff");
            enemyAsleep[0].transform.gameObject.SetActive(false);
            asleep = false;
        }
    }
}





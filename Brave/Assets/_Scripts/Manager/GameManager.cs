using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Transform respawnPoint;

    [SerializeField]
    private Transform orbPoint;

    [SerializeField]
    private GameObject orbSparks;

    [SerializeField]
    private GameObject orbSparksPos;

    [SerializeField]
    private GameObject courageOrbGet;

    [SerializeField]
    private GameObject oldPlayer;

    [SerializeField]
    private GameObject newPlayer;

    [SerializeField]
    private GameObject[] enemyAwake;

    [SerializeField]
    private GameObject[] enemyAsleep;

    [SerializeField]
    private float respawnTime;

    [SerializeField]
    private float sleepTime = 2f;

    private float orbSpawnTime = 4f;

    private float respawnTimeStart;

    private float orbSpawnTimeStart;

    private float sleepStartTime;

    private bool respawn;

    private bool orbSpawn;

    private bool orbSpawnTwo;

    private bool asleep;

    private CinemachineVirtualCamera CVC;

    private AudioManager enemyAudio;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();

        enemyAudio = GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioManager>();
    }

    private void Update()
    {
        CheckRespawn();
        CheckSleepTime();
        CheckOrbSpawn();
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }

    public void OrbSpawn()
    {
        orbSpawnTimeStart = Time.time;
        orbSpawn = true;
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
            var playerTemp = Instantiate(oldPlayer, respawnPoint);
            CVC.m_Follow = playerTemp.transform;
            respawn = false;
        }
    }

    private void CheckOrbSpawn()
    {
        if (orbSpawn)
        {
            Instantiate(orbSparks, orbSparksPos.transform.position, orbSparks.transform.rotation);
            courageOrbGet.transform.gameObject.SetActive(true);
            CVC.m_Follow = courageOrbGet.transform;
            orbSpawn = false;
            orbSpawnTwo = true;
        }
        if (Time.time >= orbSpawnTimeStart + orbSpawnTime && orbSpawnTwo)
        {
            courageOrbGet.transform.gameObject.SetActive(false);
            var playerTemp = Instantiate(newPlayer, orbPoint);
            CVC.m_Follow = playerTemp.transform;
            oldPlayer = newPlayer;
            orbSpawnTwo = false;
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





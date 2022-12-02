using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    [Header("First Selected Button")]
    [SerializeField] private GameObject firstSelected;

    public EventSystem eventSystem;
    public string exitName;
    public GameData data;
    public GameObject player;
    public PlayerInput playerInput;
    public AudioSource music;
    public AudioSource menuMusic;
    public float maxVolume = 1f;
    public float minVolume = 0f;
    public float fadeOutDuration = 2f;
    public Animator transition;

    private void Awake()
    {
        PlayerInAirState.climbAbility = false;
        PlayerWallSlideState.climbAbility = false;
        PlayerLedgeClimbState.crouchAbility = false;
        PlayerGroundedState.crouchAbility = false;
        PlayerInAirState.dashAbility = false;
        PlayerJumpState.doubleJumpAbility = false;
        PlayerInAirState.glideAbility = false;
        PlayerTouchingWallState.wallJumpAbility = false;
        PlayerLedgeClimbState.wallJumpAbility = false;

        menuMusic.ignoreListenerPause = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(SetFirstSelected(firstSelected));
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindWithTag("Player");
        playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();
        music = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
    }

    public IEnumerator SetFirstSelected(GameObject firstSelectedButton)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }
}
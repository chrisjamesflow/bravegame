using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool CanPause = true;

    public EventSystem eventSystem;
    public GameObject resumeButton;
    public GameObject pauseMenuUI;
    public GameObject player;
    public PlayerInput playerInput;
    public AudioSource pauseMusic;

    void OnEnable()
    {
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
    }

    void Update()
    {
        if (Gamepad.current.startButton.wasPressedThisFrame || Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        playerInput.ActivateInput();
        pauseMusic.Stop();
        AudioListener.pause = false;
        GameIsPaused = false;
    }

    void Pause()
    {
        if (CanPause)
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
            playerInput.DeactivateInput();
            AudioListener.pause = true;
            pauseMusic.ignoreListenerPause = true;
            pauseMusic.Play();
            GameIsPaused = true;
        }

        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(resumeButton);
    }

    public void ToMainMenu()
    {
        pauseMusic.Stop();
        SceneManager.LoadScene(0);
        MusicManager.instance.StopMusic();
    }

    public void QuitGame()
    {
        Debug.Log("Quit game!");
        Application.Quit();
    }

    public void LoadPlayer()
    {
        Resume();

        PlayerSaveData data = SaveSystem.LoadPlayer();

        PlayerInAirState.climbAbility = data.inAirClimb;
        PlayerWallSlideState.climbAbility = data.wallSlideClimb;
        PlayerLedgeClimbState.crouchAbility = data.ledgeCrouch;
        PlayerGroundedState.crouchAbility = data.groundCrouch;
        PlayerInAirState.dashAbility = data.dash;
        PlayerInAirState.glideAbility = data.glide;
        PlayerJumpState.jumpAmount = data.jumpAmount;
        PlayerTouchingWallState.wallJumpAbility = data.wallJump;
        PlayerLedgeClimbState.wallJumpAbility = data.ledgeWallJump;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        player.transform.position = position;

        PlayerPrefs.DeleteKey("LastExitName");

        MusicManager.instance.StopMusicInstantly();

        SceneManager.LoadScene(data.currentScene);
    }
}

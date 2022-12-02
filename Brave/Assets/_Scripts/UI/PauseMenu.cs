using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool CanPause = true;

    public string exitName;
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
        if (!DialogueManager.GetInstance().dialogueIsPlaying && Gamepad.current.startButton.wasPressedThisFrame || Keyboard.current.escapeKey.wasPressedThisFrame)
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
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        pauseMusic.Stop();
        MusicManager.instance.StopMusic();
        GameIsPaused = false;
        SceneManager.LoadScene(0);
        PlayerPrefs.SetString("LastExitName", exitName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game!");
        Application.Quit();
    }

    public void LoadPlayer()
    {
        Resume();
        DataPersistenceManager.instance.LoadGame();
        PlayerPrefs.DeleteKey("LastExitName");
        MusicManager.instance.StopMusicInstantly();
    }
}

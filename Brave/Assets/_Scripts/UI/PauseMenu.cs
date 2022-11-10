using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public EventSystem eventSystem;
    public GameObject resumeButton;
    public GameObject pauseMenuUI;
    public GameObject player;
    public PlayerInput playerInput;

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
        AudioListener.pause = false;
        GameIsPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        playerInput.DeactivateInput();
        AudioListener.pause = true;
        GameIsPaused = true;

        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(resumeButton);
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

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        player.transform.position = position;

        PlayerPrefs.DeleteKey("LastExitName");

        SceneManager.LoadScene(data.currentScene);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject playButton;
    public GameObject player;
    public PlayerInput playerInput;
    public string exitName;
    public AudioSource music;
    public AudioSource menuMusic;
    public GameData data;

    public float maxVolume = 1f;
    public float minVolume = 0f;
    public float fadeOutDuration = 2f;
    public Animator transition;

    public void Awake()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        PauseMenu.GameIsPaused = true;

        menuMusic.ignoreListenerPause = true;
        menuMusic.volume = 0.2f;
        menuMusic.Play();

        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(playButton); 
    }

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
        music = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
    }

    public void New()
    {
        StartCoroutine(CRNew());
    }
    
    public void Load()
    {
        StartCoroutine(CRLoad());
    }

    public void Quit()
    {
        StartCoroutine(CRQuit());
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        PauseMenu.GameIsPaused = false;
        playerInput.ActivateInput();
    }

    IEnumerator CRNew()
    {
        transition.SetTrigger("FadeOut");
        StartCoroutine(FadeOut(menuMusic, fadeOutDuration));
        yield return new WaitForSecondsRealtime(fadeOutDuration);
        Resume();

        PlayerPrefs.SetString("LastExitName", exitName);

        MusicManager.instance.StopMusicInstantly();

        DataPersistenceManager.instance.NewGame();

        DataPersistenceManager.instance.SaveGame();

        SceneManager.LoadSceneAsync(1);
    }

    IEnumerator CRLoad()
    {
        transition.SetTrigger("FadeOut");
        StartCoroutine(FadeOut(menuMusic, fadeOutDuration));
        yield return new WaitForSecondsRealtime(fadeOutDuration);
        Resume();

        PlayerPrefs.DeleteKey("LastExitName");
        MusicManager.instance.StopMusicInstantly();
        DataPersistenceManager.instance.LoadGame();
    }

    IEnumerator CRQuit()
    {
        transition.SetTrigger("FadeOut");
        StartCoroutine(FadeOut(menuMusic, fadeOutDuration));
        yield return new WaitForSecondsRealtime(fadeOutDuration);
        Debug.Log("Quit game!");
        Application.Quit();
    }

    IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.unscaledDeltaTime / fadeOutDuration;
            yield return null;
        }
    }
}



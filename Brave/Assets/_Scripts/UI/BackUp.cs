using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BackUp : MonoBehaviour
{
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

    public void Awake()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        PauseMenu.GameIsPaused = true;

        menuMusic.ignoreListenerPause = true;
        menuMusic.volume = 0.2f;
        menuMusic.Play();
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
        SceneManager.LoadScene(1);
        DataPersistenceManager.instance.SaveGame();
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
        //DataPersistenceManager.instance.SaveGame();
    }

    IEnumerator CRQuit()
    {
        transition.SetTrigger("FadeOut");
        StartCoroutine(FadeOut(menuMusic, fadeOutDuration));
        yield return new WaitForSecondsRealtime(fadeOutDuration);
        Debug.Log("Quit game!");
        Application.Quit();
    }

    public IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.unscaledDeltaTime / fadeOutDuration;
            yield return null;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        PauseMenu.GameIsPaused = false;
        playerInput.ActivateInput();
    }

    protected virtual void OnEnable()
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
}

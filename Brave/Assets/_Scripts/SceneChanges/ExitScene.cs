using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ExitScene : MonoBehaviour
{
    public int sceneToLoad;
    public string exitName;
    public float transitionTime = 0.4f;
    public Animator transition;
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
        playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetString("LastExitName", exitName);
            StartCoroutine(PlayerDeactivate());
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        MusicManager.instance.StopMusic();
        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator PlayerDeactivate()
    {
        yield return new WaitForSeconds(0.5f);
        playerInput.DeactivateInput();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class EnterScene : MonoBehaviour
{
    public string lastExitName;

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


    private void Start()
    {
        playerInput.ActivateInput();
        if (PlayerPrefs.GetString("LastExitName") == lastExitName)
        {
            PlayerManager.instance.transform.position = transform.position;
        }
    }
}

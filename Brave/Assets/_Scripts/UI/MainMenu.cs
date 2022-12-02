using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button quitGameButton;

    private void Start()
    {
        playerInput.DeactivateInput();
    }

    public void OnNewGameClicked()
    {
        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    public void OnLoadGameClicked()
    {
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    public void OnQuitGameClicked()
    {
        StartCoroutine(CRQuit());
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
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
            audioSource.volume -= startVolume * Time.unscaledDeltaTime / duration;
            yield return null;
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button backButton;

    [Header("Confirmation Popup")]
    [SerializeField] private ConfirmationPopupMenu confirmationPopupMenu;

    private SaveSlot[] saveSlots;

    private bool isLoadingGame = false;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        DisableMenuButtons();

        if (saveSlot.HasData)
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            StartCoroutine(CRLoad());
        }
        else
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            StartCoroutine(CRNew());
        }
    }

    public void OnClearClicked(SaveSlot saveSlot)
    {
        DisableMenuButtons();

        confirmationPopupMenu.ActivateMenu(
            "Are you sure you want to delete this saved data?",
            () => {
                DataPersistenceManager.instance.DeleteProfileData(saveSlot.GetProfileId());
                ActivateMenu(isLoadingGame);
            },
            () => {
                ActivateMenu(isLoadingGame);
            }
        );
    }

    public void OnBackClicked()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void ActivateMenu(bool isLoadingGame)
    {
        gameObject.SetActive(true);

        this.isLoadingGame = isLoadingGame;

        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();

        backButton.interactable = true;

        GameObject firstSelected = backButton.gameObject;
        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            if (profileData == null && isLoadingGame)
            {
                saveSlot.SetInteractable(false);
            }
            else
            {
                saveSlot.SetInteractable(true);
                if (firstSelected.Equals(backButton.gameObject))
                {
                    firstSelected = saveSlot.gameObject;
                }
            }
        }
        GameObject firstSelectedButton = firstSelected;
        StartCoroutine(SetFirstSelected(firstSelectedButton));
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    private void DisableMenuButtons()
    {
        foreach (SaveSlot saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(false);
        }
        backButton.interactable = false;
    }

    IEnumerator CRNew()
    {
        transition.SetTrigger("FadeOut");
        StartCoroutine(FadeOut(menuMusic, fadeOutDuration));
        yield return new WaitForSecondsRealtime(fadeOutDuration);
        DataPersistenceManager.instance.NewGame();
        Time.timeScale = 1f;
        AudioListener.pause = false;
        PlayerPrefs.SetString("LastExitName", exitName);
        MusicManager.instance.StopMusicInstantly();
        SceneManager.LoadScene(1);
    }

    IEnumerator CRLoad()
    {
        transition.SetTrigger("FadeOut");
        StartCoroutine(FadeOut(menuMusic, fadeOutDuration));
        Time.timeScale = 1f;
        AudioListener.pause = false;
        PlayerPrefs.DeleteKey("LastExitName");
        MusicManager.instance.StopMusicInstantly();
        yield return new WaitForSecondsRealtime(fadeOutDuration);
        DataPersistenceManager.instance.LoadGame();
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
}
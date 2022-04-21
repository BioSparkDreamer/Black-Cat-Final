using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Object Variables")]
    public GameObject[] buttons;
    public string startScene, continueScene;
    public CanvasGroup optionsMenu, creditsMenu, controlsMenu, mainMenu;
    public GameObject startMenu, continueButton;

    [Header("Object Variables")]
    private FadingScreen theFader;
    private LoadingScreen theLS;

    void Awake()
    {
        theFader = FindObjectOfType<FadingScreen>();
        theLS = FindObjectOfType<LoadingScreen>();
    }

    void Start()
    {
        //Turn the continue button on or off depending if there is a key found
        if (PlayerPrefs.HasKey(startScene + "_unlocked"))
            continueButton.SetActive(true);
        else
            continueButton.SetActive(false);
    }

    public void ChangeActiveButtons(int buttonToChose)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[buttonToChose]);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(continueScene);
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
    }

    public void OpenMenu()
    {
        startMenu.SetActive(false);
        mainMenu.alpha = 1;
        mainMenu.blocksRaycasts = true;
    }

    public void CloseMenu()
    {
        mainMenu.alpha = 0;
        mainMenu.blocksRaycasts = false;
    }

    public void OpenOptions()
    {
        optionsMenu.alpha = 1;
        optionsMenu.blocksRaycasts = true;
    }

    public void CloseOptions()
    {
        optionsMenu.alpha = 0;
        optionsMenu.blocksRaycasts = false;
    }

    public void OpenCredits()
    {
        creditsMenu.alpha = 1;
        creditsMenu.blocksRaycasts = true;
    }

    public void CloseCredits()
    {
        creditsMenu.alpha = 0;
        creditsMenu.blocksRaycasts = false;
    }

    public void OpenControls()
    {
        controlsMenu.alpha = 1;
        controlsMenu.blocksRaycasts = true;
    }

    public void CloseControls()
    {
        controlsMenu.alpha = 0;
        controlsMenu.blocksRaycasts = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFXAdjusted(0);
    }

    public void FadeToLoadScreen(string levelToLoad)
    {
        StartCoroutine(FadeToLoadCo(levelToLoad));
    }

    public IEnumerator FadeToLoadCo(string levelToLoad)
    {
        theFader.FadeToBlack();

        yield return new WaitForSeconds((1f / theFader.fadeSpeed) + .5f);

        theFader.FadeFromBlack();
        theLS.LoadScene(levelToLoad);
    }
}

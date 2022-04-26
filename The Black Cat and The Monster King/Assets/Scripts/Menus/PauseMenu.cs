using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    [Header("Pausing Variables")]
    public GameObject pauseScreen;
    public bool canPause = true;
    public GameObject resumeButton;
    [HideInInspector] public bool isPaused;

    [Header("Loading to Main Menu")]
    public string loadToMenu, loadToLevelSelect;

    [Header("Pause Menu Variables")]
    public GameObject[] buttons;
    public CanvasGroup pauseMenu, optionsMenu, controlsMenu, creditsMenu;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause") && canPause)
        {
            PauseandUnPause();
        }
    }

    public void ChangeActiveButtons(int buttonToChose)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[buttonToChose]);
    }

    public void PauseandUnPause()
    {
        //Do if Player is Pausing the Game
        if (!pauseScreen.activeInHierarchy)
        {
            pauseScreen.SetActive(true);
            OpenPauseMenu();
            isPaused = true;
            Time.timeScale = 0;

            if (PlayerController.instance != null)
                PlayerController.instance.canMove = false;

            //Make EventSystem select resume button when pausing game
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resumeButton);
        }

        //Do if player is Unpausing the Game
        else
        {
            ClosePauseMenu();
            CloseControls();
            CloseOptions();
            CloseCredits();
            Time.timeScale = 1;
            isPaused = false;

            if (PlayerController.instance != null)
                PlayerController.instance.canMove = true;

            pauseScreen.SetActive(false);
        }
    }

    public void RestartScene()
    {
        ChangeTimeScale();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevelSelect()
    {
        ChangeTimeScale();
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(loadToLevelSelect);
    }

    public void QuitToMenu()
    {
        ChangeTimeScale();
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(loadToMenu);
    }

    public void OpenPauseMenu()
    {
        pauseMenu.alpha = 1;
        pauseMenu.blocksRaycasts = true;
    }

    public void ClosePauseMenu()
    {
        pauseMenu.alpha = 0;
        pauseMenu.blocksRaycasts = false;
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

    public void ChangeTimeScale()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFXAdjusted(0);
    }
}

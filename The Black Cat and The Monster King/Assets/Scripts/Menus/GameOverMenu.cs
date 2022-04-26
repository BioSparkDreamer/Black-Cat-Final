using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour
{
    public static GameOverMenu instance;

    [Header("Game Over Variables")]
    public GameObject gameOverScreen;
    public GameObject restartButton;
    public float timeToShow;

    [Header("Game Over Menu Variables")]
    public GameObject[] buttons;
    public CanvasGroup gameOverMenu, optionsMenu, controlsMenu, creditsMenu;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GameOverScreen()
    {
        StartCoroutine(ShowGameOverScreenCO());
    }

    public IEnumerator ShowGameOverScreenCO()
    {
        PauseMenu.instance.canPause = false;
        PlayerController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(timeToShow);

        gameOverScreen.SetActive(true);
        OpenGameOverMenu();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(restartButton);

        AudioManager.instance.PlayGameOverMusic();
        Time.timeScale = 0;
    }

    public void ChangeActiveButtons(int buttonToChose)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[buttonToChose]);
    }

    public void OpenGameOverMenu()
    {
        gameOverMenu.alpha = 1;
        gameOverMenu.blocksRaycasts = true;
    }

    public void CloseGameOverMenu()
    {
        gameOverMenu.alpha = 0;
        gameOverMenu.blocksRaycasts = false;
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
}

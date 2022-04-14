using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WinScreen : MonoBehaviour
{
    [Header("Starting Game")]
    public string mainMenu;

    [Header("Menu Object Variables")]
    public GameObject[] buttons;
    public CanvasGroup optionsMenu, creditsMenu, controlsMenu;

    void Start()
    {

    }

    void Update()
    {

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void ChangeActiveButtons(int buttonToChose)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[buttonToChose]);
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

    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFXAdjusted(0);
    }
}

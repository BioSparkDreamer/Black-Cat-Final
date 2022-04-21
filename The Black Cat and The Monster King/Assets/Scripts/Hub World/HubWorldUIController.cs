using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HubWorldUIController : MonoBehaviour
{
    public static HubWorldUIController instance;

    [Header("Fade Screen Variables")]
    public float fadeSpeed;
    public Image fadeScreen;
    private bool fadeFromBlack, fadeToBlack;

    [Header("Panel Variables")]
    public GameObject levelInfoPanel;
    public TMP_Text levelName;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        FadeFromBlack();
    }

    void Update()
    {
        //Code for fading the screen to black
        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
            Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        //Code for fading the screen from black
        if (fadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
            Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }

    public void ShowInformation(HubMapPoint levelInfo)
    {
        levelName.text = levelInfo.levelName;
        levelInfoPanel.SetActive(true);
    }

    public void HideInformation()
    {
        levelInfoPanel.SetActive(false);
    }

    public void FadeFromBlack()
    {
        fadeToBlack = false;
        fadeFromBlack = true;
    }

    public void FadeToBlack()
    {
        fadeFromBlack = false;
        fadeToBlack = true;
    }
}

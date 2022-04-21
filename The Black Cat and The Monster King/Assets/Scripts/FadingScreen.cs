using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingScreen : MonoBehaviour
{
    [Header("Fading Image Variables")]
    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeFromBlack, fadeToBlack;

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
                fadeScreen.raycastTarget = false;
            }
        }
    }

    public void FadeFromBlack()
    {
        fadeToBlack = false;
        fadeFromBlack = true;
        fadeScreen.raycastTarget = true;
    }

    public void FadeToBlack()
    {
        fadeFromBlack = false;
        fadeToBlack = true;
        fadeScreen.raycastTarget = true;
    }
}

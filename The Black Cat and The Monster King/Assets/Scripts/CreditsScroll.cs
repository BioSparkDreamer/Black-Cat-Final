using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CreditsScroll : MonoBehaviour
{
    [Header("Object Variables")]
    private FadingScreen theFader;
    public float waitTime;
    public bool hasEnded;
    public string sceneToLoad;

    void Start()
    {
        theFader = FindObjectOfType<FadingScreen>();
    }

    void Update()
    {
        waitTime -= Time.deltaTime;

        if (waitTime <= 0f && !hasEnded)
        {
            hasEnded = true;
            FadeAndLoadScene();
        }

        if (Input.anyKeyDown && !hasEnded)
        {
            hasEnded = true;
            FadeAndLoadScene();
        }
    }

    public void FadeAndLoadScene()
    {
        StartCoroutine(FadeAndLoadSceneCo());
    }

    public IEnumerator FadeAndLoadSceneCo()
    {
        theFader.FadeToBlack();

        yield return new WaitForSeconds((1f / theFader.fadeSpeed) + 0.5f);

        SceneManager.LoadScene(sceneToLoad);
    }
}

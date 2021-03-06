using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source Variables")]
    public AudioSource[] sfx;
    public AudioSource levelMusic, menuMusic, cutSceneMusic, bossMusic;
    public bool isLevel, isMenu, isCutscene;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        if (isLevel)
            levelMusic.Play();

        if (isMenu)
            menuMusic.Play();

        if (isCutscene)
            cutSceneMusic.Play();

    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

    public void PlaySFXAdjusted(int sfxToPlay)
    {
        sfx[sfxToPlay].pitch = Random.Range(.8f, 1.2f);
        sfx[sfxToPlay].Play();
    }

    public void StopMusic()
    {
        levelMusic.Stop();
        menuMusic.Stop();
        cutSceneMusic.Stop();
        bossMusic.Stop();
    }

    public void PlayBossMusic()
    {
        StopMusic();
        bossMusic.Play();
    }

    public void StopBossMusic()
    {
        StopMusic();
        levelMusic.Play();
    }

    public void PlayGameOverMusic()
    {
        StopMusic();
        menuMusic.Play();
    }
}


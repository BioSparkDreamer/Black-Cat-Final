using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("CheckPoint Variables")]
    private CheckPoint[] checkPoints;
    public Vector3 spawnPoint;

    [Header("Soul Collecing System Variables")]
    public int maxSouls = 50;
    public int currentSouls;
    public GameObject soulParticleEffect;

    [Header("Loading Next Level")]
    public string nextLevel;
    public bool levelIsEnding;

    [Header("Respawning")]
    public float waitToRespawn;

    [Header("Object Variables")]
    private FadingScreen theFader;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        theFader = FindObjectOfType<FadingScreen>();
    }

    void Start()
    {
        checkPoints = FindObjectsOfType<CheckPoint>();
        spawnPoint = PlayerController.instance.transform.position;
    }

    void Update()
    {

    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    public void DeactiveCheckPoints()
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].ResetCheckPoint();
        }
    }

    public void UpdateSoulsCount(int soulsToAdd)
    {
        currentSouls += soulsToAdd;

        if (currentSouls >= maxSouls)
        {
            currentSouls = maxSouls;
        }

        UIController.instance.UpdateSoulUI();
    }

    public void SubtractSouls()
    {
        currentSouls = 0;
        AudioManager.instance.PlaySFXAdjusted(10);
        Instantiate(soulParticleEffect, PlayerController.instance.transform.position, Quaternion.identity);

        UIController.instance.UpdateSoulUI();
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCO());
    }

    private IEnumerator RespawnCO()
    {
        PlayerHealthController.instance.theSR.enabled = false;
        PauseMenu.instance.canPause = false;
        UIController.instance.isDead = true;

        yield return new WaitForSeconds(waitToRespawn - (1 / theFader.fadeSpeed));

        theFader.FadeToBlack();

        yield return new WaitForSeconds((1f / theFader.fadeSpeed) + 1f);

        theFader.FadeFromBlack();

        PlayerController.instance.transform.position = spawnPoint;
        PlayerHealthController.instance.theSR.enabled = true;
        PlayerController.instance.currentStamina = PlayerController.instance.maxStamina;
        UIController.instance.UpdateStaminaUI();
        PauseMenu.instance.canPause = true;
        UIController.instance.isDead = false;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCO());
    }

    private IEnumerator EndLevelCO()
    {
        PauseMenu.instance.canPause = false;
        PlayerController.instance.canMove = false;

        levelIsEnding = true;
        PlayerController.instance.theRB.velocity = new Vector2(5f, PlayerController.instance.theRB.velocity.y);

        yield return new WaitForSeconds(2f);
        theFader.FadeToBlack();
        yield return new WaitForSeconds((1f / theFader.fadeSpeed) + 1f);

        StoreSaveData();

        SceneManager.LoadScene(nextLevel);
    }

    public void StoreSaveData()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
    }
}

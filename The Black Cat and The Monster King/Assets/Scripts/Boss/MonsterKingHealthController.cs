using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterKingHealthController : MonoBehaviour
{
    public static MonsterKingHealthController instance;

    [Header("Health Variables")]
    public Slider bossHealthSlider;
    public int currentHealth;
    public int maxHealth = 100;
    private MonsterKingBossController theBoss;
    public int soulsToAdd = 10;
    public TMP_Text bossText;
    public GameObject hitEffect;
    public Transform bossLocation;

    [Header("Sprite Related Variables")]
    public SpriteRenderer theSR;
    public float transparentPerHit, spriteAlphaValue;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        currentHealth = maxHealth;
    }

    void Start()
    {
        theBoss = FindObjectOfType<MonsterKingBossController>();

        UpdateBossUI();
    }

    public void TakeDamage(int damageToTake)
    {
        currentHealth -= damageToTake;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            theBoss.EndBattle();
        }

        if (GameManager.instance.currentSouls < GameManager.instance.maxSouls)
        {
            GameManager.instance.currentSouls += soulsToAdd;
            UIController.instance.UpdateSoulUI();
        }

        if (currentHealth >= 20)
        {
            theSR.color = new Color(1f, 1f, 1f, spriteAlphaValue - transparentPerHit);
            spriteAlphaValue = theSR.color.a;
        }

        if (hitEffect != null)
            Instantiate(hitEffect, bossLocation.transform.position, bossLocation.transform.rotation);

        UpdateBossUI();
    }

    public void UpdateBossUI()
    {
        bossText.text = "MONSTER KING: " + currentHealth + "/" + maxHealth;

        bossHealthSlider.maxValue = maxHealth;
        bossHealthSlider.value = currentHealth;
    }
}

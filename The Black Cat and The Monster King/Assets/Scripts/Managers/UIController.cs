using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("Health Variables")]
    public Slider healthSlider;
    public TMP_Text healthText;

    [Header("Stamina Variables")]
    public Slider staminaSlider;
    public TMP_Text staminaText;

    [Header("Soul Collect Variables")]
    public Slider soulSlider;
    public TMP_Text soulText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        UpdateHealthUI();
        UpdateStaminaUI();
        UpdateSoulUI();
    }

    public void UpdateHealthUI()
    {
        healthSlider.maxValue = PlayerHealthController.instance.maxHealth;
        healthSlider.value = PlayerHealthController.instance.currentHealth;

        healthText.text = "LIVES: " + PlayerHealthController.instance.currentHealth.ToString()
        + "/" + PlayerHealthController.instance.maxHealth.ToString();
    }

    public void UpdateStaminaUI()
    {
        staminaSlider.maxValue = PlayerController.instance.maxStamina;
        staminaSlider.value = PlayerController.instance.currentStamina;

        if (PlayerController.instance.currentStamina >= PlayerController.instance.maxStamina)
        {
            PlayerController.instance.currentStamina = PlayerController.instance.maxStamina;
        }

        staminaText.text = "STAMINA: " + PlayerController.instance.currentStamina.ToString("F0");
    }

    public void UpdateSoulUI()
    {
        soulSlider.maxValue = GameManager.instance.maxSouls;
        soulSlider.value = GameManager.instance.currentSouls;

        soulText.text = "SOULS: " + GameManager.instance.currentSouls.ToString() + "/" + GameManager.instance.maxSouls.ToString();
    }
}

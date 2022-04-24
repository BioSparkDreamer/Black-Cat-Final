using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int health, soulsToAdd;
    public GameObject enemyObject;
    public GameObject deathEffect;
    public bool isMonster, isSlime, isBat;

    [Header("Sprite Color Variables")]
    private SpriteRenderer theSR;
    public Color defaultColor, hurtColor;
    public float hurtTime;
    private float hurtCounter;

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        defaultColor = theSR.color;
    }

    void Update()
    {
        if (hurtCounter > 0)
        {
            hurtCounter -= Time.deltaTime;
            theSR.color = hurtColor;

            if (hurtCounter <= 0)
            {
                theSR.color = defaultColor;
            }
        }
    }

    public void TakeDamage(int damageToDeal)
    {
        health -= damageToDeal;
        hurtCounter = hurtTime;

        if (health <= 0)
        {
            health = 0;
            Destroy(enemyObject);
            GameManager.instance.UpdateSoulsCount(soulsToAdd);

            if (isBat)
            {
                AudioManager.instance.PlaySFXAdjusted(1);
            }
            if (isMonster)
            {
                AudioManager.instance.PlaySFXAdjusted(2);
            }
            if (isSlime)
            {
                AudioManager.instance.PlaySFXAdjusted(5);
            }

            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingBossController : MonoBehaviour
{
    [Header("Object Variables")]
    private BossCameraController theCam;
    public Animator anim;
    public Transform camPosition;
    public float camSpeed;
    public Transform theBoss;

    [Header("Phase Variables")]
    public int threshold1;
    public int thereshold2;
    public float activeTime, fadeOutTime, inactiveTime, deathTime;
    private float activeCounter, fadeCounter, inactiveCounter, deathCounter;

    [Header("Spawn Point Variables")]
    public Transform[] spawnPoints;
    private Transform targetPoint;
    public float moveSpeed, fasterMoveSpeed;

    [Header("Shooting Variables")]
    public float timeBetweenShots1;
    public float timeBetweenShots2;
    private float shotCounter;
    public GameObject theBullet;
    public Transform firePoint;

    [Header("Ending Boss Variables")]
    private bool battleEnded;
    public GameObject blockObject;

    void Start()
    {
        theCam = FindObjectOfType<BossCameraController>();
        theCam.enabled = false;
        AudioManager.instance.PlayBossMusic();

        activeCounter = activeTime;
        shotCounter = timeBetweenShots1;
    }

    void Update()
    {
        //Move the Camera to the position
        theCam.transform.position = Vector3.MoveTowards(theCam.transform.position, camPosition.position, camSpeed * Time.deltaTime);

        if (!battleEnded)
        {
            if (theBoss.transform.position.x > PlayerController.instance.transform.position.x)
                theBoss.transform.localScale = Vector3.one;
            else
                theBoss.transform.localScale = new Vector3(-1f, 1f, 1f);


            if (MonsterKingHealthController.instance.currentHealth > threshold1)
            {
                BossPhase1();
            }
            else
            {
                BossPhase2();
            }
        }
        else
        {
            deathCounter -= Time.deltaTime;

            if (deathCounter <= 0)
            {
                AudioManager.instance.StopBossMusic();
                blockObject.SetActive(false);
                theCam.enabled = true;
                gameObject.SetActive(false);
            }
        }
    }

    public void BossPhase1()
    {
        if (activeCounter > 0)
        {
            activeCounter -= Time.deltaTime;

            if (activeCounter <= 0)
            {
                fadeCounter = fadeOutTime;
                anim.SetTrigger("vanish");
            }

            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots1;
                Instantiate(theBullet, firePoint.position, Quaternion.identity);
            }

        }
        else if (fadeCounter > 0)
        {
            fadeCounter -= Time.deltaTime;

            if (fadeCounter <= 0)
            {
                theBoss.gameObject.SetActive(false);
                inactiveCounter = inactiveTime;
            }
        }
        else if (inactiveCounter > 0)
        {
            inactiveCounter -= Time.deltaTime;

            if (inactiveCounter <= 0)
            {
                for (int i = 0; i < 3; i++)
                    theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                theBoss.gameObject.SetActive(true);
                activeCounter = activeTime;
                shotCounter = timeBetweenShots1;
            }
        }
    }

    public void BossPhase2()
    {
        if (targetPoint == null)
        {
            targetPoint = theBoss;
            fadeCounter = fadeOutTime;
            anim.SetTrigger("vanish");
        }
        else
        {
            if (Vector3.Distance(theBoss.position, targetPoint.position) > 0.02f)
            {
                theBoss.position = Vector3.MoveTowards(theBoss.position, targetPoint.position, fasterMoveSpeed * Time.deltaTime);

                if (Vector3.Distance(theBoss.position, targetPoint.position) < 0.02f)
                {
                    fadeCounter = fadeOutTime;
                    anim.SetTrigger("vanish");
                }

                shotCounter -= Time.deltaTime;

                if (shotCounter <= 0)
                {
                    if (MonsterKingHealthController.instance.currentHealth > thereshold2)
                    {
                        shotCounter = timeBetweenShots1;
                    }
                    else
                    {
                        shotCounter = timeBetweenShots2;
                    }

                    Instantiate(theBullet, firePoint.position, Quaternion.identity);
                }
            }
            else if (fadeCounter > 0)
            {
                fadeCounter -= Time.deltaTime;

                if (fadeCounter <= 0)
                {
                    theBoss.gameObject.SetActive(false);
                    inactiveCounter = inactiveTime;
                }
            }
            else if (inactiveCounter > 0)
            {
                inactiveCounter -= Time.deltaTime;

                if (inactiveCounter <= 0)
                {
                    theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                    targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                    int whileBreaker = 0;
                    while (targetPoint.position == theBoss.position && whileBreaker < 100)
                    {
                        targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                        whileBreaker++;
                    }

                    if (MonsterKingHealthController.instance.currentHealth > thereshold2)
                    {
                        shotCounter = timeBetweenShots1;
                    }
                    else
                    {
                        shotCounter = timeBetweenShots2;
                    }

                    theBoss.gameObject.SetActive(true);
                }
            }
        }
    }

    public void EndBattle()
    {
        battleEnded = true;
        AudioManager.instance.PlaySFXAdjusted(2);
        deathCounter = deathTime;
        anim.SetBool("isDead", battleEnded);
        theBoss.GetComponent<Collider2D>().enabled = false;

        MonsterKingShotController[] shots = FindObjectsOfType<MonsterKingShotController>();

        if (shots.Length > 0)
        {
            foreach (MonsterKingShotController shot in shots)
            {
                Destroy(shot.gameObject);
            }
        }
    }
}

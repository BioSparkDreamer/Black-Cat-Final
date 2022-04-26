using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject bossToActivate;
    public GameObject doorBlocker;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            bossToActivate.SetActive(true);
            doorBlocker.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

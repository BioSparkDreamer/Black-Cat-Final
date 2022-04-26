using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingShotController : MonoBehaviour
{
    [Header("Object Variables")]
    private Rigidbody2D theRB;

    [Header("Shooting Variables")]
    public float moveSpeed;
    public int damageAmount;
    public GameObject impactEffect;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        AudioManager.instance.PlaySFXAdjusted(12);

        Vector3 direction = transform.position - PlayerController.instance.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void FixedUpdate()
    {
        theRB.velocity = -transform.right * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.TakeDamage(damageAmount);
            AudioManager.instance.PlaySFXAdjusted(11);
        }

        if (impactEffect != null)
            Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will handle object destruction

public class Destructible : MonoBehaviour
{
    [Header("Health Setting")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("References")]
    public GameObject healthBarPrefab; // UI that appears on hit
    private GameObject activeHealthBar;

    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            StartCoroutine(DestroySelf());
        }
    }

    IEnumerator DestroySelf()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = Color.red; // Shows damage effect

        yield return new WaitForSeconds(0.2f);

        Destroy(gameObject);
    }
}

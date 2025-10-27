using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will handle enemy logic

public class Enemy : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 50f;
    private float currentHealth;

    [Header("References")]
    private Rigidbody2D rb;

    void start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            StartCoroutine(HandleDeath());
        }
    }

    IEnumerator HandleDeath()
    {
        // Turn red before dying
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.red;
        }
        yield return new WaitForSeconds(0.2f);

        // Notify GameManager that an enemy died
        GameManager.Instance.EnemyDefeated();

        Destroy(gameObject);
    }
}

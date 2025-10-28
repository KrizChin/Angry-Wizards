using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will handle enemy logic

public class Enemy : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 40f;
    private float currentHealth;

    [Header("References")]
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [Header("SFX")]
    public AudioSource deathSFX; 

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        deathSFX = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
            Debug.Log(gameObject.name + " took " + damage + " damage. Remaining: " + currentHealth);


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

        if (deathSFX != null)
        {
            deathSFX.pitch = Random.Range(0.9f, 1.1f);
            deathSFX.Play();
        }

        yield return new WaitForSeconds(deathSFX.clip.length);

        Destroy(gameObject);
        // Notify GameManager that an enemy died
        GameManager.Instance.EnemyDefeated();
    }
}

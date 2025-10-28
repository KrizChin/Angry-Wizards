using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles the spell projectile's lifetime and collision behavior.

public class SpellProjectile : MonoBehaviour
{
    public float lifetime = 7f;
    public float damage = 20f;
    public bool destroyOnImpact = true;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Damage destructibles
        Destructible destructible = collision.collider.GetComponent<Destructible>();
        if (destructible != null)
        {
            destructible.TakeDamage(damage);
        }
        
        // Damage enemies
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        if (destroyOnImpact)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.activeProjectiles--;
        }
    }
}

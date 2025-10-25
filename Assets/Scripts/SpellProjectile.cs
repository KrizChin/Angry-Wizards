using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles the spell projectile's lifetime and collision behavior.

public class SpellProjectile : MonoBehaviour
{
    public float lifetime = 7f;
    public float impactDamage = 20f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollision(Collision2D collision)
    {
        //Future Logic
        Destroy(gameObject);
    }
}

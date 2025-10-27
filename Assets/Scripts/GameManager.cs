using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script keeps track of total enemies and if the player wins.

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int totalEnemies;
    private int defeatedEnemies;

    void Awake()
    {
        // Makes sure there is only one GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Count all enemies when the scene starts
        totalEnemies = FindObjectsOfType<Enemy>().Length;
        defeatedEnemies = 0;
    }

    public void EnemyDefeated()
    {
        defeatedEnemies++;

        if (defeatedEnemies >= totalEnemies)
        {
            Debug.Log("You Win!");
            // ADD win screen later
        }
    }
}

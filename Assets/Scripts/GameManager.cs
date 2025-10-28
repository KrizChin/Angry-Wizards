using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

// This script keeps track of total enemies and if the player wins.

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int totalEnemies;
    private int defeatedEnemies;

    public TMP_Text victoryText;

    public LosePanel losePanel;

    public bool gameActive = true;
    public bool gameOver = false; // prevents lose screen on last shot

    public int activeProjectiles = 0;
    public PlayerMana playerMana;

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

        if (gameOver)
        {
            return;
        }

        if (defeatedEnemies >= totalEnemies)
        {
            Debug.Log("You Win!");
            gameOver = true;
            gameActive = false;
            if (victoryText != null)
            {
                victoryText.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("VictoryText reference missing in GameManager!");
            }
        }
    }

    public void PlayerLost()
    {
        if (gameOver) 
        {
            return;
        }

        Debug.Log("You Lose! :(");
        gameActive = false; // stops player input
        gameOver = true;

        if(losePanel != null)
        {
            losePanel.gameObject.SetActive(true);
        }
    }

    public void CheckForLoss()
    {
        if (playerMana.currentMana <= 0 && activeProjectiles <= 0 && !gameOver)
        {
            PlayerLost();
        }
    }
}

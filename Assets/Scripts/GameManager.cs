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

    [Header("Audio")]
    public AudioSource loseSFX;

    public WinPanel winPanel;

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
            StartCoroutine(HandleWinDelay());
        }
    }

    private IEnumerator HandleWinDelay()
    {
        gameOver = true;
        gameActive = false;
        yield return new WaitForSeconds(2.5f);
        if (winPanel != null)
        {
            winPanel.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("VictoryText reference missing in GameManager!");
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

        if (loseSFX != null)
        {
            loseSFX.Play();
        }

        if(losePanel != null)
        {
            losePanel.gameObject.SetActive(true);
        }
    }

    public void CheckForLoss()
    {
        if (gameOver)
        {
            return;
        }

        if (playerMana.currentMana <= 0 && activeProjectiles <= 0)
        {
            if (defeatedEnemies >= totalEnemies)
            {
                EnemyDefeated();
            }
            else
            {
                PlayerLost();
            }
        }
    }
}

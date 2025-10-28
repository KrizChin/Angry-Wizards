using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This scipt will handle the lose panel and make it appear when the player runs out of mana.

public class LosePanel : MonoBehaviour
{
    [Header("Buttons")]
    public Button restartButton;
    public Button mainMenuButton;

    void start()
    {
        restartButton.onClick.AddListener(RestartLevel);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    public void RestartLevel()
    {
        // Reloads the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        // Debug test
        restartButton.onClick.AddListener(() => Debug.Log("Restart button clicker:"));
    }

    public void GoToMainMenu()
    {
        // Loads the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}

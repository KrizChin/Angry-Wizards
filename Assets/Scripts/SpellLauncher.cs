using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script allows the player to launch and charge spells towards the aimed direction.

public class SpellLauncher : MonoBehaviour
{
    public GameObject spellPrefab;
    public Transform launchPoint;
    public float launchForce = 7f;
    private float chargeTime = 0f;
    private TrajectoryLine trajectoryLine;
    private PlayerMana playerMana;
    private AudioSource audioSource;

    void Start()
    {
        trajectoryLine = GetComponent<TrajectoryLine>();
        // Find the trajectory component in the GameObject.
        playerMana = GetComponentInParent<PlayerMana>();
        audioSource = GetComponent<AudioSource>(); // sfx
    }
    void Update()
    {
        // Prevent the player from shooting when game is not active (i.e. pause menu)
        if (GameManager.Instance != null && !GameManager.Instance.gameActive)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            if (playerMana == null || !playerMana.CanCast())
            {
                trajectoryLine.HideTrajectory(); // Hides line if out of mana
                return;
            }

            chargeTime += Time.deltaTime;
            //if left mouse button is held then increases target time every frame.
            float finalForce = Mathf.Clamp(launchForce * (1 + chargeTime), launchForce, launchForce * 3);
            Vector2 velocity = (Vector2)(launchPoint.right * finalForce);
            trajectoryLine.ShowTrajectory(launchPoint.position, velocity);
            // these three lines create a visual prediction of where the spell will go.
        }
        if (Input.GetMouseButtonUp(0))
            {
                if (playerMana != null && playerMana.CanCast())
                {
                    LaunchSpell();
                    playerMana.SpendMana();
                }
                else
                {
                    Debug.Log("Not enough mana!");
                }
                chargeTime = 0f;
                trajectoryLine.HideTrajectory();
            }
    }

    void LaunchSpell()
    {
        float finalForce = Mathf.Clamp(launchForce * (1 + chargeTime), launchForce, launchForce * 3);
        //Calculates the strength of the spell based on charge time with a max limit.

        if (audioSource != null)
        {
            audioSource.pitch = Random.Range(1.2f, 1.4f);
            audioSource.Play();
        }

        GameObject spell = Instantiate(spellPrefab, launchPoint.position, launchPoint.rotation);
        //Spawns a copy of the spell prefab at the launchPoint location and rotation.
        Rigidbody2D rb = spell.GetComponent<Rigidbody2D>();
        //Gets the rigidbody component of the spawned spell so we can apply physics to it.
        rb.AddForce(launchPoint.right * finalForce, ForceMode2D.Impulse);
        //Applies an instantaneous force to the spell in the direction the player is facing.

        GameManager.Instance.activeProjectiles++;
    }
}

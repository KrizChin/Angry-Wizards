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

    void Start()
    {
        trajectoryLine = GetComponent<TrajectoryLine>();
        // Find the trajectory component in the GameObject.
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            chargeTime += Time.deltaTime;
            //if left mouse button is held then increases target time every frame.
            float finalForce = Mathf.Clamp(launchForce * (1 + chargeTime), launchForce, launchForce * 3);
            Vector2 velocity = (Vector2)(launchPoint.right * finalForce);
            
            trajectoryLine.ShowTrajectory(launchPoint.position, velocity);
        }
        if (Input.GetMouseButtonUp(0))
            {
                LaunchSpell();
                chargeTime = 0f;
            trajectoryLine.HideTrajectory();
                //If the button is realeased then it calls LaunchSpell function and resets charge time.
            }
    }

    void LaunchSpell()
    {
        float finalForce = Mathf.Clamp(launchForce * (1 + chargeTime), launchForce, launchForce * 3);
        //Calculates the strength of the spell based on charge time with a max limit.
        GameObject spell = Instantiate(spellPrefab, launchPoint.position, launchPoint.rotation);
        //Spawns a copy of the spell prefab at the launchPoint location and rotation.
        Rigidbody2D rb = spell.GetComponent<Rigidbody2D>();
        //Gets the rigidbody component of the spawned spell so we can apply physics to it.
        rb.AddForce(launchPoint.right * finalForce, ForceMode2D.Impulse);
        //Applies an instantaneous force to the spell in the direction the player is facing.
    }
}

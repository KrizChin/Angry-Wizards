using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script rotates the player to face the mouse cursor allowing the player to aim.

public class PlayerAim : MonoBehaviour
{
    Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Converts the mouse position from the mainCamera to game world.
        Vector2 direction = mousePos - transform.position;
        //Calculates the direction vector from player to mouse to figure out where to rotate player.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Mathf.Atan2 gets the angle in radians and converts it into degrees.
        //Compute the angle between the direction and x-axis using trigonometry.
        transform.rotation = Quaternion.Euler(0, 0, angle);
        //Apply the rotation to the player on the z-axis.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellLauncher : MonoBehaviour
{
    public GameObject spellPrefab;
    public Transform launchPoint;
    public float launchForce = 10f;
    private float chargeTime = 0f;
    void Update()
    {
        if (Input.GetMouseButton(0))
            chargeTime += Time.deltaTime;

        if (Input.GetMouseButtonUp(0))
        {
            LaunchSpell();
            chargeTime = 0f;
        }
    }

    void LaunchSpell()
    {
        float finalForce = Mathf.Clamp(launchForce * (1 + chargeTime), launchForce, launchForce * 3);
        GameObject spell = Instantiate(spellPrefab, launchPoint.position, launchPoint.rotation);
        Rigidbody2D rb = spell.GetComponent<Rigidbody2D>();
        rb.AddForce(launchPoint.right * finalForce, ForceMode2D.Impulse);
    }
}

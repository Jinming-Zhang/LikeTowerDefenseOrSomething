using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Apply to towers and base

public class PlayerObjectHealth : MonoBehaviour
{

    public float health = 15f;
    public bool isBase = false; //set to true if it is the base; this is for the battery, and we dont need the base to be powered.
    public float energyUsage = 10f;
    [Space(15)]
    public int metalCost = 0;
    // Added here for UI because it's the script shared between the battery and turrets. - Dax


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Turn on gravity and disable rigidbody lock here
        Destroy(gameObject);
    }

    [Space(15)]
    // Dax's edits for the UI's sake:
    public string displayName = ""; // This is for the UI until we get sprites or other images to represent the object.
    public float maxHealth = 15f; // For HP bar scaling.
    public bool startsAtMaxHealth = true; // Disable if this is a problem for some reason.

    public void Start()
    { if(startsAtMaxHealth) { health = maxHealth; } }
}


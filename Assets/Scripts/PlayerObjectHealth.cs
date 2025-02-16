using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Apply to towers and base

public class PlayerObjectHealth : MonoBehaviour
{
    public float health = 15f;
    public bool isBase = false; //set to true if it is the base; this is for the battery, and we dont need the base to be powered.
    public float energyUsage = 10f;

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
}


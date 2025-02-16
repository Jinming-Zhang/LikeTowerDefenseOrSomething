using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Apply to Enemies

public class EnemyHealth : MonoBehaviour
{
    public float health = 15f;
    public bool spawnEnemyOnDeath = false;
    public GameObject SpawnOnDeathEnemy;

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
        if(spawnEnemyOnDeath) 
        {
            Instantiate(SpawnOnDeathEnemy);
        }
        Destroy(gameObject);
    }
}


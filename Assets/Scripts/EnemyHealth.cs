using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Apply to Enemies

public class EnemyHealth : HealthComponent
{
    public Resources resources;
    public float health;
    public float maxHealth = 15f;
    public bool spawnEnemyOnDeath = false;
    public GameObject SpawnOnDeathEnemy;

    public void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (resources != null && resources.gainFromDamage)
        {
            resources.amount += 1;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Turn on gravity and disable rigidbody lock here
        if (spawnEnemyOnDeath)
        {
            Instantiate(SpawnOnDeathEnemy);
        }

        if (resources != null && resources.gainFromKill)
        {
            resources.amount += maxHealth;
        }

        Destroy(gameObject);
    }

    public override float GetMaxHealth()
    {
        return maxHealth;
    }

    public override float GetCurrentHealth()
    {
        return health;
    }
}
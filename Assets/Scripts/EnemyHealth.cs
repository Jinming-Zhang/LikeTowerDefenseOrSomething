using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthComponent
{
    public Resources resources;
    [HideInInspector]
    public float health;
    public float maxHealth = 15f;
    public bool spawnEnemyOnDeath = false;
    public GameObject SpawnOnDeathEnemy;
    public bool explodeOnDeath = false;

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
        if (explodeOnDeath)
        {
            Collider[] playerColliders = Physics.OverlapSphere(transform.position, 25f);
            foreach (Collider playerCollider in playerColliders)
            {
                if (playerCollider.CompareTag("PlayerObject"))
                {
                    PlayerObjectHealth playerObjHealth = playerCollider.GetComponent<PlayerObjectHealth>();
                    if (playerObjHealth != null)
                    {
                        playerObjHealth.TakeDamage(10f);
                    }
                }
            }
        }

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

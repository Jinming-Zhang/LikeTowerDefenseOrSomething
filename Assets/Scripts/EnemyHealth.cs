using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : HealthComponent
{
    public Resources resources;
    [HideInInspector]
    public float health;
    public float maxHealth = 15f;
    public bool spawnEnemyOnDeath = false;
    public EnemyBase SpawnOnDeathEnemy;
    public bool explodeOnDeath = false;
    [Header("Sfx")]
    [SerializeField] private float _AttackedSfxCd = 0.1f;
    [SerializeField] private List<AudioClip> _AttackedSfxs = new();
    [SerializeField] private List<AudioClip> _DeadSfxs = new();
    private float _AttacedSfxCdCounter;
    private bool _DeathSfxPlayed = false;

    public void Awake()
    {
        health = maxHealth;
        _AttacedSfxCdCounter = _AttackedSfxCd;
    }

    private void Update()
    {
        _AttacedSfxCdCounter = Mathf.Max(0, _AttacedSfxCdCounter - Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        PlayDamagedSFX();
        if (resources != null && resources.gainFromDamage)
        {
            resources.amount += 1;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void PlayDamagedSFX()
    {
        if (_AttacedSfxCdCounter <= 0)
        {
            AudioManager.Instance.PlaySFXRandom(_AttackedSfxs);
            _AttacedSfxCdCounter = _AttackedSfxCd;
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
            EnemyBase enemy = Instantiate(SpawnOnDeathEnemy);
            enemy.Initialize(GetComponent<NavAIMovement>().GetCurrentPathNode());
        }

        if (resources != null && resources.gainFromKill)
        {
            resources.amount += maxHealth;
        }

        if (!_DeathSfxPlayed)
        {
            AudioManager.Instance?.PlaySFXRandom(_DeadSfxs);
            _DeathSfxPlayed = true;
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
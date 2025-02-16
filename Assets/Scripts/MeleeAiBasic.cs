using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAiBasic : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    public Transform Base;
    public bool basePriority = true;

    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Attacking")]
    public float damage = 2f;
    public float critChance = 0.05f;
    public float attackRange = 3f;
    public float attackSpeed = 2f;
    public float attackCooldownTimer = 0f;

    [Header("Spawning")]
    public bool spawnEnemies = false;
    public GameObject enemyToSpawn;
    public float spawnCooldown = 5f;
    private float spawnCooldownTimer = 0f;

    private Vector3 targetPosition;

    void Start()
    {
        if (basePriority)
        {
            target = Base;
        }
    }

    void Update()
    {
        if (!basePriority)
        {
            FindTarget();
        }
        LookAtTarget();
        Move();
        AttemptAttackTarget();
        HandleEnemySpawning();
    }

    public void FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("PlayerObject"))
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = hitCollider.transform;
                }
            }
        }

        if (closestTarget != null)
        {
            target = closestTarget;
        }
    }

    public void LookAtTarget()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }
    }

    public void Move()
    {
        Vector3 direction = (transform.position - target.position).normalized;
        targetPosition = target.position + direction;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    public void AttemptAttackTarget()
    {
        attackCooldownTimer -= Time.deltaTime;

        if (target != null && Vector3.Distance(transform.position, target.position) <= attackRange && attackCooldownTimer <= 0)
        {
            attackCooldownTimer = attackSpeed;

            PlayerObjectHealth targetHealth = target.GetComponent<PlayerObjectHealth>();

            float damageToDeal = damage;

            if (Random.Range(0f, 1f) <= critChance)
            {
                damageToDeal *= 1.2f;
            }

            targetHealth.TakeDamage(damageToDeal);
        }
    }

    private void HandleEnemySpawning()
    {
        if (spawnEnemies)
        {
            spawnCooldownTimer -= Time.deltaTime;

            if (spawnCooldownTimer <= 0f)
            {
                SpawnEnemy();
                spawnCooldownTimer = spawnCooldown;
            }
        }
    }

    private void SpawnEnemy()
    {
        if (enemyToSpawn != null)
        {
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }
    }


}

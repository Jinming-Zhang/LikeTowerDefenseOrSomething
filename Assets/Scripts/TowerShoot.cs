using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    public float damage = 10f;
    public float reloadTime = 2f;
    public float range = 10f;
    public Transform shootPoint;
    public float batteryRange = 10f;
    public float batteryCapacity = 50f;
    public int shootAmount = 1;
    public bool trap = false;
    public Transform pivotPoint;

    private float timeSinceLastShot = 0f;
    private Collider currentTarget = null;
    private Battery nearbyBattery = null;
    private float batteryUsed = 0f;
    public List<Transform> turretHeads = new List<Transform>();
    [SerializeField] private ProjectileWeapon _Weapon;

    [Header("SFX")]
    [SerializeField] private List<AudioClip> _ShootSFXs;
    [SerializeField] private float _SFXVolume = 1.0f;

    public Battery NearbyBattery
    {
        get { return nearbyBattery; }
    }

    void Start()
    {
        if (!trap)
        {
            FindAndSelectBattery();
        }
    }

    void Update()
    {
        if (nearbyBattery == null)
        {
            FindAndSelectBattery();
        }

        if (!trap && nearbyBattery == null) return;

        timeSinceLastShot += Time.deltaTime;

        if (!trap)
        {
            AdjustStatsBasedOnBatteryUsage();
        }

        if (trap)
        {
            DamageAllEnemiesInRange();
            return;
        }

        if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.transform.position) > range)
        {
            currentTarget = FindEnemyInRange();
        }

        if (currentTarget != null && timeSinceLastShot >= reloadTime)
        {
            Turret turret = GetComponent<Turret>();
            if (turret != null)
            {
                turret.RotateToTarget(currentTarget.transform);
            }

            StartCoroutine(DealDamageToEnemy(currentTarget, shootAmount));
            timeSinceLastShot = 0f;
        }
        else if (currentTarget == null)
        {
            currentTarget = FindEnemyInRange();
        }
    }

    void FindAndSelectBattery()
    {
        if (trap) return;

        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, batteryRange);
        Battery bestBattery = null;
        float highestAvailablePower = -1f;

        foreach (Collider collider in collidersInRange)
        {
            if (collider.CompareTag("PlayerObject"))
            {
                Battery battery = collider.GetComponent<Battery>();
                if (battery != null)
                {
                    float availablePower = battery.capacity - battery.used;
                    if (availablePower > highestAvailablePower)
                    {
                        highestAvailablePower = availablePower;
                        bestBattery = battery;
                    }
                }
            }
        }

        if (bestBattery != null)
        {
            nearbyBattery = bestBattery;
            transform.SetParent(nearbyBattery.transform);
        }
    }

    void AdjustStatsBasedOnBatteryUsage()
    {
        if (nearbyBattery != null)
        {
            batteryUsed = nearbyBattery.used;
            if (batteryUsed > batteryCapacity)
            {
                float usageFactor = batteryUsed / batteryCapacity;
                damage /= usageFactor;
                reloadTime *= usageFactor;
            }
        }
    }

    Collider FindEnemyInRange()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, range);
        float closestDistance = Mathf.Infinity;
        Collider closestEnemy = null;

        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }

    IEnumerator DealDamageToEnemy(Collider enemyCollider, int shootAmount)
    {
        EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            for (int i = 0; i < shootAmount - 1; i++)
            {
                yield return new WaitForSeconds(0.2f);
                enemyHealth.TakeDamage(damage);
                if (_Weapon != null)
                {
                    AudioManager.Instance?.PlaySFXRandom(_ShootSFXs, _SFXVolume, _Weapon.transform.position);
                    _Weapon.Shoot(enemyCollider.gameObject);
                }
            }
        }
    }

    void DamageAllEnemiesInRange()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, range);
        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }
    }

    public void ShootAtTarget(Transform target)
    {
        if (trap) return;
        Collider targetCollider = target.GetComponent<Collider>();
        if (targetCollider != null)
        {
            currentTarget = targetCollider;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, batteryRange);
    }
}
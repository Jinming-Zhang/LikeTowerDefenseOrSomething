using System;
using System.Collections;
using UnityEngine;

public class LaserWeapon : Weapon
{
    [SerializeField] protected float _Radius = 0.2f;
    [SerializeField] private float _DmgPerSec = 2;
    [Header("Visuals")]
    [SerializeField] private LaserLine _LaserVisual;


    private GameObject _CurrentTarget;

    [Header("Rotation Settings")]
    [SerializeField] private Transform _Pivot;

    private void Awake()
    {
        _LaserVisual.SetActive(false);
    }

    public override void Shoot(GameObject target)
    {
        if (_CurrentTarget == target)
        {
            return;
        }

        _CurrentTarget = target;
        StopAllCoroutines();
        StartCoroutine(ShootCR());
    }

    private IEnumerator ShootCR()
    {
        _LaserVisual.SetLaserPoints(transform, _CurrentTarget.transform);
        _LaserVisual.SetActive(true);

        while (IsTargetValid())
        {
            Vector3 laserDirection = _CurrentTarget.transform.position - transform.position;
            _LaserVisual.SetLaserPoints(transform, _CurrentTarget.transform);
            float dmg = _DmgPerSec * Time.deltaTime;

            RaycastHit[] hits = Physics.SphereCastAll(transform.position, _Radius, laserDirection, laserDirection.magnitude, _DamageLayers);
            foreach (RaycastHit hit in hits)
            {
                EnemyHealth health = hit.collider.gameObject.GetComponent<EnemyHealth>();
                if (health != null)
                {
                    health.TakeDamage(dmg);
                }
            }

            yield return new WaitForEndOfFrame();
        }

        _CurrentTarget = null;
        _LaserVisual.SetActive(false);
    }

    private bool IsTargetValid()
    {
        if (_CurrentTarget == null)
        {
            return false;
        }

        if (Vector3.Distance(_CurrentTarget.transform.position, transform.position) > _MaxRange)
        {
            return false;
        }

        EnemyHealth health = _CurrentTarget.GetComponent<EnemyHealth>();
        if (health != null && health.health <= 0)
        {
            return false;
        }

        return true;
    }


    private void RotateTowardsTarget()
    {
        if (_Pivot != null && _CurrentTarget != null)
        {
            Vector3 direction = _CurrentTarget.transform.position - _Pivot.position;
            direction.y = 0;
            direction.x -= 90;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _Pivot.rotation = lookRotation;
        }
    }

    private void Update()
    {
        RotateTowardsTarget();
    }
}
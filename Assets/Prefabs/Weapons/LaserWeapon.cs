using System;
using System.Collections;
using UnityEngine;

public class LaserWeapon : Weapon
{
    [Header("Weapon Config")]
    [SerializeField] private float _Radius = 0.2f;

    [SerializeField] private float _MaxRange = 10.0f;
    [SerializeField] private float _DmgPerSec = 2;
    [SerializeField] private LayerMask _DamageLayers;

    [Header("Visuals")]
    [SerializeField] private LaserLine _LaserVisual;

    [Header("Debug")]
    [SerializeField] private Color _DebugRangeColor = Color.red;

    private GameObject _CurrentTarget;

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
                    health.health -= dmg;
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

    private void OnDrawGizmosSelected()
    {
        var oldClr = Gizmos.color;
        Gizmos.color = _DebugRangeColor;
        Gizmos.DrawWireSphere(transform.position, _MaxRange);
        Gizmos.color = oldClr;
    }
}
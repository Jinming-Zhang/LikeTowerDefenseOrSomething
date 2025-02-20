using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float _Range;

    public float range
    {
        get { return _Range; }
    } // I need to read this for UI purposes - Dax

    [SerializeField] private LayerMask _TargetLayers;

    [Header("Weapon")]
    [SerializeField] private Transform _WeaponSocket;

    [SerializeField] private Weapon _Weapon;


    [SerializeField] private List<GameObject> _TurretHeads;

    [Header("Debug")]
    [SerializeField] private Color _GizmosColor = Color.cyan;

    // Update is called once per frame
    void Update()
    {
        Transform target = CheckTargets();
        if (target != null)
        {
            ShootTarget(target);
        }
    }

    private Transform CheckTargets()
    {
        Collider[] hits =
            Physics.OverlapSphere(transform.position, _Range, _TargetLayers);
        if (hits.Length <= 0)
        {
            return null;
        }

        List<Collider> hitList = hits.ToList();
        hitList.Sort((c1, c2) =>
        {
            return Vector3.Distance(c1.transform.position, transform.position) <
                   Vector3.Distance(c2.transform.position, transform.position)
                ? -1
                : 1;
        });

        Transform target = hitList[0].transform;
        return target;
    }

    private void ShootTarget(Transform target)
    {
        _Weapon.Shoot(target.gameObject);
        foreach (GameObject turretHead in _TurretHeads)
        {
            RotateToTarget(turretHead.transform, target);
        }
    }

    private void RotateToTarget(Transform src, Transform target)
    {
        Vector3 targetDirection = target.position - src.position;
        targetDirection.y = 0;
        targetDirection.x += 0;

        if (targetDirection.sqrMagnitude > 0f)
        {
            src.rotation = Quaternion.LookRotation(targetDirection);
        }
    }


    private void OnDrawGizmos()
    {
        if (_Range <= 0)
        {
            return;
        }

        Color oldColor = Gizmos.color;
        Gizmos.color = _GizmosColor;
        Gizmos.DrawWireSphere(transform.position, _Range);
        Gizmos.color = oldColor;
    }
}
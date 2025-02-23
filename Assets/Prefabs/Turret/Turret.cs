using System;
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

    [SerializeField] private bool _RotateRoot;
    [Header("Debug")]
    [SerializeField] private Color _GizmosColor = Color.cyan;

    private void Start()
    {
        if (_Weapon != null)
        {
            _Weapon.SetRange(_Range);
        }
    }

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
        if (_Weapon == null)
        {
            return;
        }

        _Weapon.Shoot(target.gameObject);
        foreach (GameObject turretHead in _TurretHeads)
        {
            RotateToTarget(turretHead.transform, target);
        }
    }

    public void RotateToTarget(Transform target)
    {
        if (_RotateRoot)
        {
            Vector3 dir = target.position - transform.position;
            dir.y = transform.position.y;
            transform.forward = dir;
            // float dot = Vector3.Dot(transform.forward, dir.normalized);
            // float angle = Mathf.Acos(dot);
            // bool rotLeft = Vector3.Cross(dir.normalized, transform.forward).y <= 0;
            // angle = rotLeft ? angle : -angle;
            // transform.Rotate(0, angle, 0);
        }
        else
        {
            foreach (GameObject turretHead in _TurretHeads)
            {
                RotateToTarget(turretHead.transform, target);
            }
        }
    }

    private void RotateToTarget(Transform src, Transform target)
    {
        if (_RotateRoot)
        {
            Vector3 lookDir = target.position - transform.position;
            lookDir.y = transform.position.y;
            transform.forward = lookDir;
        }
        else
        {
            Vector3 tarloc = target.position;
            tarloc.y = transform.position.y;
            Vector3 targetRight = src.position - tarloc;
            float dot = Vector3.Dot(src.right.normalized, targetRight.normalized);
            float angle = Mathf.Acos(dot);
            bool rotLeft = Vector3.Cross(targetRight, src.right).y <= 0;
            angle = rotLeft ? angle : -angle;
            src.RotateAround(transform.position, Vector3.up, angle);
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
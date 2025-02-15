using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class Turret : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float _Range;

    [SerializeField] private LayerMask _TargetLayers;

    [Header("Weapon")]
    [SerializeField] private Transform _WeaponSocket;

    [SerializeField] private LaserLine _LaserLine;

    [Header("Debug")]
    [SerializeField] private Color _GizmosColor = Color.cyan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargets();
    }

    private void CheckTargets()
    {
        Collider[] hits =
            Physics.OverlapSphere(transform.position, _Range, _TargetLayers);
        if (hits.Length <= 0)
        {
            if (_LaserLine != null)
            {
                _LaserLine.gameObject.SetActive(false);
            }

            return;
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
        if (_LaserLine == null)
        {
            return;
        }

        _LaserLine.gameObject.SetActive(true);
        _LaserLine.SetLaserPoints(_WeaponSocket, target);
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
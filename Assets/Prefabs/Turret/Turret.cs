using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float _Range;
    public float range { get { return _Range; } } // I need to read this for UI purposes - Dax

    [SerializeField] private LayerMask _TargetLayers;

    [Header("Weapon")]
    [SerializeField] private Transform _WeaponSocket;

    [SerializeField] private Weapon _Weapon;


    [Header("Debug")]
    [SerializeField] private Color _GizmosColor = Color.cyan;

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
        _Weapon.Shoot(target.gameObject);
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
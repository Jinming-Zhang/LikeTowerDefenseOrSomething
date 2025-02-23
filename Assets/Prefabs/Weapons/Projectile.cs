using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject _Target;
    private Vector3 _Velocity;
    private float _Ttl;

    public void Initialize(GameObject target, Vector3 velocity, float ttl)
    {
        _Target = target;
        _Velocity = velocity;
        _Ttl = ttl;
    }

    void Update()
    {
        if (_Target == null)
        {
            return;
        }

        _Ttl -= Time.deltaTime;
        if (_Ttl <= 0)
        {
            Destroy(gameObject);
        }

        _Velocity = (_Target.transform.position - transform.position).normalized * _Velocity.magnitude;

        if (Vector3.Distance(transform.position, _Target.transform.position) < 10)
        {
            Destroy(gameObject);
        }

        transform.position += _Velocity * Time.deltaTime;
        transform.forward = _Velocity.normalized;
    }
}
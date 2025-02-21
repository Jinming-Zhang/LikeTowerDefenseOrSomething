using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackModule : MonoBehaviour
{
    private float _Damage = 3;
    private float _AtkSpeedPerSec = 2;

    private float _AtkCdCounter;
    [SerializeField] private UnityEvent OnAttack;

    public void Initialize(float dmg, float speed)
    {
        _AtkSpeedPerSec = speed;
        _Damage = dmg;
        _AtkCdCounter = 0;
    }

    public void Attack(EnemyAttackable target)
    {
        if (CanAttack())
        {
            DoAttack(target);
        }
    }

    private bool CanAttack()
    {
        return _AtkCdCounter <= 0;
    }

    private void DoAttack(EnemyAttackable target)
    {
        Vector3 forwardDir = target.transform.position - transform.position;
        forwardDir.y = transform.position.y;
        transform.forward = forwardDir;
        _AtkCdCounter = 1.0f / _AtkSpeedPerSec;
        target.TakeDamage(_Damage);
        OnAttack?.Invoke();
    }

    private void Update()
    {
        _AtkCdCounter = Mathf.Max(0, _AtkCdCounter - Time.deltaTime);
    }
}
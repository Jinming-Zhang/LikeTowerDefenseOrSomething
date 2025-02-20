using System;
using UnityEngine;

public class EnemyAttackModule : MonoBehaviour
{
    private float _Damage = 3;
    private float _AtkSpeedPerSec = 2;

    private float _AtkCdCounter;

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
        _AtkCdCounter = 1.0f / _AtkSpeedPerSec;
        target.TakeDamage(_Damage);
    }

    private void Update()
    {
        _AtkCdCounter = Mathf.Max(0, _AtkCdCounter - Time.deltaTime);
    }
}
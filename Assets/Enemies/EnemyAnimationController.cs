using System;
using UnityEngine;

[RequireComponent(typeof(EnemyBase))]
public class EnemyAnimationController : MonoBehaviour
{
    private EnemyBase _EnemyBase;
    [SerializeField] private Animator _EnemyAnimator;

    private void Awake()
    {
        _EnemyBase = GetComponent<EnemyBase>();
    }

    private void Update()
    {
        switch (_EnemyBase.EnemyStatus)
        {
            case EnemyStatus.Idling:
                OnIdle();
                break;
            case EnemyStatus.Walking:
                OnMoving();
                break;
            default:
                break;
        }
    }

    public void OnAttacking()
    {
        _EnemyAnimator.SetTrigger("Attack");
    }

    public void OnIdle()
    {
        _EnemyAnimator.Play("Idle");
    }

    public void OnMoving()
    {
        _EnemyAnimator.Play("Move");
    }
}
using UnityEngine;

[RequireComponent(typeof(NavAIMovement),
    typeof(EnemyAttackModule),
    typeof(EnemyHealth)
)]
public class EnemyBase : MonoBehaviour
{
    [Header("Enemy Configs")]
    [SerializeField] private float _Health = 2;
    [SerializeField] private float _Damage = 3;
    [SerializeField] private float _MoveSpeed = 2;
    [SerializeField] private float _AtkSpeedPerSec = 2;
    [SerializeField] private float _AtkRange = 20;

    private NavAIMovement _MovementModule;
    private EnemyAttackModule _AttackModule;
    [Header("Debug")]
    [SerializeField] private Color _RangeColor = Color.red;

    private void Awake()
    {
        _MovementModule = GetComponent<NavAIMovement>();
        _AttackModule = GetComponent<EnemyAttackModule>();
    }

    public void Initialize(PathNode navPath)
    {
        _MovementModule.Initialize(_MoveSpeed, navPath);
        _AttackModule.Initialize(_Damage, _AtkSpeedPerSec);
    }


    void Update()
    {
        if (TryGetAttackTarget(out EnemyAttackable target))
        {
            _MovementModule.Pause();
            _AttackModule.Attack(target);
        }
        else
        {
            _MovementModule.Resume();
        }
    }

    bool TryGetAttackTarget(out EnemyAttackable target)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _AtkRange);
        foreach (Collider hit in hits)
        {
            EnemyAttackable attackable = hit.GetComponent<EnemyAttackable>();
            if (attackable != null)
            {
                target = attackable;
                return true;
            }
        }

        target = null;
        return false;
    }


    private void OnDrawGizmosSelected()
    {
        Color oldClr = Gizmos.color;
        Gizmos.color = _RangeColor;
        Gizmos.DrawWireSphere(transform.position, _AtkRange);
        Gizmos.color = oldClr;
    }
}
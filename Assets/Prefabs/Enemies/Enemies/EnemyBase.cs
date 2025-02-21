using System.ComponentModel;
using UnityEngine;

public enum EnemyStatus
{
    Idling,
    Walking,
    Attacking
}

[RequireComponent(typeof(NavAIMovement),
    typeof(EnemyAttackModule),
    typeof(EnemyHealth)
)]
public class EnemyBase : MonoBehaviour
{
    [Header("Basic Enemy Configs")]
    [SerializeField] private float _Damage = 3;
    [SerializeField] private float _MoveSpeed = 2;

    [Header("Other Configs")]
    [SerializeField] private bool _StraightToBase = false;
    [SerializeField] private float _AtkSpeedPerSec = 2;
    [SerializeField] private float _AtkRange = 20;

    public EnemyStatus EnemyStatus { get; set; }
    private NavAIMovement _MovementModule;
    private EnemyAttackModule _AttackModule;

    [Header("Spawning")]
    public bool spawnEnemies = false;
    public GameObject enemyToSpawn;
    public float spawnCooldown = 5f;
    private float spawnCooldownTimer = 0f;

    [Header("Debug")]
    [SerializeField] private Color _RangeColor = Color.red;

    private void Awake()
    {
        _MovementModule = GetComponent<NavAIMovement>();
        _AttackModule = GetComponent<EnemyAttackModule>();
        EnemyStatus = EnemyStatus.Idling;
        _MovementModule.SetSpeed(_MoveSpeed);
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
            EnemyStatus = EnemyStatus.Attacking;
        }
        else
        {
            _MovementModule.Resume();
            EnemyStatus = EnemyStatus.Walking;
            HandleEnemySpawning();
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
                if (!_StraightToBase || attackable.GetComponent<BaseNexus>() != null)
                {
                    target = attackable;
                    return true;
                }
            }
        }

        target = null;
        return false;
    }


    private void HandleEnemySpawning()
    {
        if (spawnEnemies && enemyToSpawn != null)
        {
            spawnCooldownTimer -= Time.deltaTime;

            if (spawnCooldownTimer <= 0f)
            {
                Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
                spawnCooldownTimer = spawnCooldown;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Color oldClr = Gizmos.color;
        Gizmos.color = _RangeColor;
        Gizmos.DrawWireSphere(transform.position, _AtkRange);
        Gizmos.color = oldClr;
    }
}
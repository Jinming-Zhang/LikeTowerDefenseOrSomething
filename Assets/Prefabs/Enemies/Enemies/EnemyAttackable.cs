using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackable : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> _OnTakeDamage;

    public void TakeDamage(float dmg)
    {
        _OnTakeDamage?.Invoke(dmg);
    }
}
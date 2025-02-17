using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthBarWorldSpace : MonoBehaviour
{
    [SerializeField] private EnemyHealth _HealthTarget;
    private float _InitialHealth;
    private float _CurrentHealth;

    [SerializeField]
    private Image _Foreground;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (_HealthTarget == null)
        {
            return;
        }

        _CurrentHealth = _HealthTarget.health;
        _Foreground.fillAmount = Mathf.Clamp01(_CurrentHealth / _InitialHealth);
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

    private void Init()
    {
        if (_HealthTarget == null)
        {
            _HealthTarget = GetComponent<EnemyHealth>();
            if (_HealthTarget == null)
            {
                _HealthTarget = GetComponentInParent<EnemyHealth>();
            }

            if (_HealthTarget == null)
            {
                Debug.LogError(
                    $"HealthBarUI: Cannot find {_HealthTarget.GetType().Name} component to show health!");
                return;
            }
        }

        _InitialHealth = _HealthTarget.health;
        _CurrentHealth = _InitialHealth;
        _Foreground.fillAmount = _CurrentHealth / _InitialHealth;
    }

    public void Reset()
    {
        Init();
    }
}
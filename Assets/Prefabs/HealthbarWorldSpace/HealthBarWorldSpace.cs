using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarWorldSpace : MonoBehaviour
{
    private EnemyHealth _Health;
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
        if (_Health == null)
        {
            return;
        }

        _CurrentHealth = _Health.health;
        _Foreground.fillAmount = Mathf.Clamp01(_CurrentHealth / _InitialHealth);
    }

    private void Init()
    {
        _Health = GetComponent<EnemyHealth>();
        if (_Health == null)
        {
            _Health = GetComponentInParent<EnemyHealth>();
        }

        if (_Health == null)
        {
            Debug.LogError(
                $"HealthBarUI: Cannot find {_Health.GetType().Name} component to show health!");
            return;
        }

        _InitialHealth = _Health.health;
        _CurrentHealth = _InitialHealth;
        _Foreground.fillAmount = _CurrentHealth / _InitialHealth;
    }

    public void Reset()
    {
        Init();
    }
}
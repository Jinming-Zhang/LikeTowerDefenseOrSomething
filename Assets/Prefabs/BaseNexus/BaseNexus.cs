using UnityEngine;

public class BaseNexus : HealthComponent
{
    [SerializeField] private float _MaxHealth;
    private float _CurHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _CurHealth = _MaxHealth;
    }

    public void TakeDamage(float dmg)
    {
        _CurHealth -= dmg;
        if (_CurHealth <= 0)
        {
            // SendMessageUpwards("NexusDestroyed");
        }
    }

    public override float GetMaxHealth()
    {
        return _MaxHealth;
    }

    public override float GetCurrentHealth()
    {
        return _CurHealth;
    }
}
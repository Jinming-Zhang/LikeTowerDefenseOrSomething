using UnityEngine;

public class TurretHealth : HealthComponent
{
    [SerializeField] private float _Health;
    private float _CurHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _CurHealth = _Health;
    }

    public void TakeDmg(float dmg)
    {
        _CurHealth -= dmg;
        if (_CurHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override float GetMaxHealth()
    {
        return _Health;
    }

    public override float GetCurrentHealth()
    {
        return _CurHealth;
    }
}
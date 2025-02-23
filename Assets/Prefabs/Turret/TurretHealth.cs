using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : HealthComponent
{
    [SerializeField] private float _Health;
    private float _CurHealth;
    [Header("SFXs")]
    [SerializeField] private float _DamagedSfxCd = 0.15f;
    [SerializeField] private List<AudioClip> _DamagedSfxs = new();
    [SerializeField] private List<AudioClip> _DeadSfxs = new();
    private float _DamagedSfxCdCounter;
    private bool _IsKilledSfxPlayed = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _CurHealth = _Health;
        _DamagedSfxCdCounter = _DamagedSfxCd;
        _IsKilledSfxPlayed = false;
        if (pHealth != null)
        {
            pHealth.health = _Health;
            pHealth.maxHealth = _Health;
        }
    }

    private void Update()
    {
        _DamagedSfxCdCounter = Mathf.Max(0, _DamagedSfxCdCounter - Time.deltaTime);
    }

    public void TakeDmg(float dmg)
    {
        _CurHealth -= dmg;
        pHealth?.TakeDamage(dmg);
        PlayDamagedSfx();
        if (_CurHealth <= 0)
        {
            if (!_IsKilledSfxPlayed)
            {
                AudioManager.Instance?.PlaySFXRandom(_DeadSfxs);
                _IsKilledSfxPlayed = true;
            }

            Destroy(gameObject);
        }
    }

    private void PlayDamagedSfx()
    {
        if (_DamagedSfxCdCounter <= 0)
        {
            AudioManager.Instance?.PlaySFXRandom(_DamagedSfxs);
            _DamagedSfxCdCounter = _DamagedSfxCd;
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
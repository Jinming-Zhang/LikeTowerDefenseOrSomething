using System;
using System.Collections;
using UnityEngine;

public class LaserWeapon : Weapon
{
    [SerializeField] private float _DmgPerShoot = 1;
    [SerializeField] private float _ShootDuration = 0.3f;
    [SerializeField] private LaserLine _LaserVisual;
    [SerializeField] private float _ShootCd = 1;
    private float _CdCounter;

    private void Awake()
    {
        _LaserVisual.SetActive(false);
        _CdCounter = 0;
    }

    public override void Shoot(GameObject target)
    {
        if (_CdCounter > 0)
        {
            return;
        }

        _CdCounter = _ShootCd;
        StartCoroutine(ShootCR(target));
    }

    private IEnumerator ShootCR(GameObject target)
    {
        float timer = _ShootDuration;
        float dmgPerSec = _DmgPerShoot / _ShootDuration;
        _LaserVisual.SetLaserPoints(transform, target.transform);
        _LaserVisual.SetActive(true);
        do
        {
            yield return new WaitForEndOfFrame();
            _LaserVisual.SetLaserPoints(transform, target.transform);
            timer -= Time.deltaTime;
            _CdCounter -= Time.deltaTime;
            float dmg = dmgPerSec * Time.deltaTime;
            if (target != null)
            {
                EnemyHealth health = target.GetComponent<EnemyHealth>();
                health.health -= dmg;
            }
        } while (timer > 0);

        _LaserVisual.SetActive(false);
        if (_CdCounter > 0)
        {
            yield return new WaitForSeconds(_CdCounter);
            _CdCounter = 0;
        }
    }
}
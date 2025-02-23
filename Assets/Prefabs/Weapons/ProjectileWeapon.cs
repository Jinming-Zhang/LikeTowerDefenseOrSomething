using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private float _Speed;
    [SerializeField] private float _ProjectileTTL;
    [SerializeField] private Projectile _Projectile;

    public override void Shoot(GameObject target)
    {
        Vector3 dir = target.transform.position - transform.position;
        Projectile p = Instantiate(_Projectile, transform.position, Quaternion.identity);
        p.Initialize(target, dir.normalized * _Speed, _ProjectileTTL);
    }
}
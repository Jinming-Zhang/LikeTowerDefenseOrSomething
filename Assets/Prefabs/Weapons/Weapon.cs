using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Config")]
    protected float _MaxRange = 10.0f;
    [SerializeField] protected LayerMask _DamageLayers;

    public void SetRange(float range)
    {
        _MaxRange = range;
    }

    public abstract void Shoot(GameObject target);
}
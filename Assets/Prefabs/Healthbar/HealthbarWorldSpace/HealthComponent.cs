using UnityEngine;

public abstract class HealthComponent : MonoBehaviour
{
    protected PlayerObjectHealth pHealth
    {
        get { return GetComponent<PlayerObjectHealth>(); }
    }

    public abstract float GetMaxHealth();
    public abstract float GetCurrentHealth();
}
using UnityEngine;

public abstract class HealthComponent : MonoBehaviour
{
    public abstract float GetMaxHealth();
    public abstract float GetCurrentHealth();
}
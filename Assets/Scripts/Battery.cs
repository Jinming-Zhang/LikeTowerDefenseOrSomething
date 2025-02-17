using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : PlayerObjectHealth
{
    public float range = 10f;
    public float capacity = 50f;
    public float used = 0f;

    void Update()
    {
        used = 0f; 
        CheckForPlayerObjectInChildren();
    }

    void CheckForPlayerObjectInChildren()
    {
        foreach (Transform child in transform)
        {
            PlayerObjectHealth playerObjectHealth = child.GetComponent<PlayerObjectHealth>();
            if (playerObjectHealth != null && !playerObjectHealth.isBase)
            {
                used += playerObjectHealth.energyUsage;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

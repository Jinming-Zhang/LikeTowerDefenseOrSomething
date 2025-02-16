using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage = 10f;
    public float speed = 15f;
    private Vector3 targetPosition;
    private bool hasHit = false;

    public void Initialize(Vector3 targetPos, float projectileDamage, float projectileSpeed)
    {
        targetPosition = targetPos;
        damage = projectileDamage;
        speed = projectileSpeed;
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        if (!hasHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                OnHitTarget();
            }
        }
    }

    void OnHitTarget()
    {
        hasHit = true;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("PlayerObject"))
            {
                PlayerObjectHealth targetHealth = hitCollider.GetComponent<PlayerObjectHealth>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(damage);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasHit && collision.gameObject.CompareTag("PlayerObject"))
        {
            OnHitTarget();
        }
    }
}

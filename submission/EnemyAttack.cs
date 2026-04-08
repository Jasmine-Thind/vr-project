using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 10f;
    public float attackRate = 1f;
    private float nextAttackTime = 0f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time >= nextAttackTime)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                    playerHealth.TakeDamage(damage);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
}
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 10;
    public float damageCooldown = 1f;

    private float nextDamageTime = 0f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }
}
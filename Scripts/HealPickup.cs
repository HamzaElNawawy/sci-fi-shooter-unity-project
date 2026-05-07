using UnityEngine;

public class HealPickup : MonoBehaviour
{
    public int healAmount = 4;

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
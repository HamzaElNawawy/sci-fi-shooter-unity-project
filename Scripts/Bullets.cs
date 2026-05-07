using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;

    // This tells bullet who fired it: "Player" or "Enemy"
    public string ownerTag;

    void OnCollisionEnter(Collision collision)
    {
        // Do not damage the object that shot the bullet
        if (collision.gameObject.CompareTag(ownerTag))
        {
            Destroy(gameObject);
            return;
        }

        // Level 1 targets
        Target target = collision.gameObject.GetComponentInParent<Target>();
        if (target != null && ownerTag == "Player")
        {
            target.Hit();
            Destroy(gameObject);
            return;
        }

        // Enemy takes damage only from player bullets
        EnemyHealth enemy = collision.gameObject.GetComponentInParent<EnemyHealth>();
        if (enemy != null && ownerTag == "Player")
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // Player takes damage only from enemy bullets
        PlayerHealth player = collision.gameObject.GetComponentInParent<PlayerHealth>();
        if (player != null && ownerTag == "Enemy")
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        Destroy(gameObject);
    }
}
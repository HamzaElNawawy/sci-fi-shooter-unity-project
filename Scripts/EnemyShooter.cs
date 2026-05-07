using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float shootForce = 20f;
    public float fireRate = 2f;
    public AudioClip shootSound;

    private float nextFireTime;

    void Update()
    {
        if (player == null || bulletPrefab == null || firePoint == null) return;

        LookAtPlayer();

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void Shoot()
    {
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }

        // aim at the player's chest, not feet
        Vector3 targetPoint = player.position + Vector3.up * 1.2f;
        Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(shootDirection)
        );

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.ownerTag = "Enemy";
        }

        Collider bulletCollider = bullet.GetComponent<Collider>();
        Collider enemyCollider = GetComponent<Collider>();

        if (bulletCollider != null && enemyCollider != null)
        {
            Physics.IgnoreCollision(bulletCollider, enemyCollider);
        }

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * shootForce;
        }

        Destroy(bullet, 3f);
    }
}
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public Slider healthBar;

    public AudioClip deathSound;
    public GameObject deathEffect;

    public GameProgressManager progressManager;

    public bool isLevel2Enemy;
    public bool isLevel3Enemy;
    public bool isBoss;

    private int currentHealth;
    private bool isDead = false;

    void OnEnable()
    {
        currentHealth = maxHealth;
        isDead = false;

        if (healthBar != null)
        {
            healthBar.minValue = 0;
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (deathSound != null)
            AudioSource.PlayClipAtPoint(deathSound, transform.position);

        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        if (progressManager != null)
        {
            if (isLevel2Enemy)
                progressManager.Level2EnemyDestroyed();

            if (isLevel3Enemy)
                progressManager.Level3EnemyDestroyed();

            if (isBoss)
                progressManager.BossDefeated();
        }

        gameObject.SetActive(false);
    }
}
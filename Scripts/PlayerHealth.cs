using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;

    public Slider healthBar;
    public Image healthFill;

    public AudioSource damageSource;
    public AudioClip damageSound;
    public AudioClip healSound;

    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Player health: " + currentHealth + "/" + maxHealth);

        if (damageSource != null && damageSound != null)
        {
            damageSource.PlayOneShot(damageSound);
        }

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Player healed: " + currentHealth + "/" + maxHealth);

        if (damageSource != null && healSound != null)
        {
            damageSource.PlayOneShot(healSound);
        }

        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.minValue = 0;
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (healthFill != null)
        {
            healthFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        isDead = true;

        Debug.Log("PLAYER DEAD");

        if (animator != null)
        {
            animator.SetBool("Dead", true);
        }

        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false;

        PlayerShooting shooting = GetComponent<PlayerShooting>();
        if (shooting != null)
            shooting.enabled = false;
    }
}
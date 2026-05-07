using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class PlayerShooting : MonoBehaviour
{
    public Camera playerCamera;
    public CinemachineCamera cinemachineCamera;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 30f;

    [Header("Aim Settings")]
    public float normalFOV = 60f;
    public float aimFOV = 25f;
    public float aimSpeed = 12f;
    public GameObject scopeUI;
    public GameObject crosshairUI;

    [Header("Audio")]
    public AudioSource shootSource;
    public AudioClip shootSound;

    [Header("Effects")]
    public GameObject shootFX;

    private Animator animator;
    private bool isAiming;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (scopeUI != null)
            scopeUI.SetActive(false);

        if (crosshairUI != null)
            crosshairUI.SetActive(true);

        if (cinemachineCamera != null)
            cinemachineCamera.Lens.FieldOfView = normalFOV;

        if (playerCamera != null)
            playerCamera.fieldOfView = normalFOV;
    }

    void Update()
    {
        HandleAiming();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void HandleAiming()
    {
        isAiming = Input.GetMouseButton(1);

        if (animator != null)
            animator.SetBool("Aiming", isAiming);

        float targetFOV = isAiming ? aimFOV : normalFOV;

        if (cinemachineCamera != null)
        {
            cinemachineCamera.Lens.FieldOfView =
                Mathf.Lerp(cinemachineCamera.Lens.FieldOfView, targetFOV, Time.deltaTime * aimSpeed);
        }

        if (playerCamera != null)
        {
            playerCamera.fieldOfView =
                Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * aimSpeed);
        }

        if (scopeUI != null)
            scopeUI.SetActive(isAiming);

        // Crosshair stays visible while aiming
        if (crosshairUI != null)
            crosshairUI.SetActive(true);
    }

    void Shoot()
    {
        if (animator != null)
            animator.SetTrigger("Shoot");

        if (shootSource != null && shootSound != null)
            shootSource.PlayOneShot(shootSound);

        if (shootFX != null)
        {
            ParticleSystem ps = shootFX.GetComponent<ParticleSystem>();

            if (ps != null)
            {
                ps.Stop();
                ps.Play();
            }
        }

        if (playerCamera == null || bulletPrefab == null || firePoint == null) return;

        Ray aimRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 targetPoint;

        if (Physics.Raycast(aimRay, out RaycastHit hit, 200f))
        {
            targetPoint = hit.point;

            Debug.Log("Ray hit: " + hit.collider.name);

            EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }

            Target target = hit.collider.GetComponentInParent<Target>();
            if (target != null)
            {
                target.Hit();
            }
        }
        else
        {
            targetPoint = aimRay.GetPoint(200f);
        }

        Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(shootDirection)
        );

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.ownerTag = "Player";
        }

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * bulletSpeed;
        }

        Destroy(bullet, 3f);
    }

    void Reload()
    {
        if (animator != null)
        {
            animator.SetBool("Reloading", true);
            Invoke(nameof(StopReload), 1.5f);
        }
    }

    void StopReload()
    {
        if (animator != null)
            animator.SetBool("Reloading", false);
    }
}
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject hitEffect;
    public AudioClip hitSound;
    public GameProgressManager progressManager;

    private bool alreadyHit = false;

    public void Hit()
    {
        if (alreadyHit) return;

        alreadyHit = true;

        if (hitEffect != null)
            Instantiate(hitEffect, transform.position, Quaternion.identity);

        if (hitSound != null)
            AudioSource.PlayClipAtPoint(hitSound, transform.position);

        if (progressManager != null)
            progressManager.Level1TargetDestroyed();

        gameObject.SetActive(false);
    }
}
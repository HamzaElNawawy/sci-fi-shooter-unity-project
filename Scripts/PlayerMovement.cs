using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 7f;
    public float mouseSensitivity = 300f;
    public float gravity = -20f;

    public float rollSpeed = 8f;
    public float rollDuration = 0.6f;

    public Transform cameraTarget;

    public AudioSource footstepSource;
    public AudioSource rollSource;

    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip rollSound;

    private CharacterController controller;
    private Animator animator;
    private float xRotation = 0f;
    private Vector3 velocity;
    private bool isRolling = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -35f, 60f);

        if (cameraTarget != null)
            cameraTarget.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isMoving = x != 0 || z != 0;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving;

        // ROLL (Q)
        if (Input.GetKeyDown(KeyCode.Q) && !isRolling)
        {
            Vector3 rollDirection = transform.right * x + transform.forward * z;

            if (rollDirection == Vector3.zero)
                rollDirection = transform.forward;

            StartCoroutine(Roll(rollDirection.normalized));
        }

        // Normal movement
        if (!isRolling)
        {
            float currentSpeed = isRunning ? runSpeed : walkSpeed;

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * currentSpeed * Time.deltaTime);

            HandleFootsteps(isMoving, isRunning);
        }
        else
        {
            HandleFootsteps(false, false);
        }

        // Gravity
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        HandleAnimation(x, z, isRunning);
    }

    IEnumerator Roll(Vector3 direction)
    {
        isRolling = true;

        if (animator != null)
            animator.SetTrigger("Roll");

        // 🔊 Play roll sound (SEPARATE SOURCE)
        if (rollSource != null && rollSound != null)
        {
            rollSource.PlayOneShot(rollSound);
        }

        float timer = 0f;

        while (timer < rollDuration)
        {
            controller.Move(direction * rollSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        isRolling = false;
    }

    void HandleAnimation(float x, float z, bool isRunning)
    {
        if (animator == null) return;

        animator.SetFloat("X", x);
        animator.SetFloat("Y", z);
        animator.SetFloat("Speed", new Vector2(x, z).magnitude);
        animator.SetBool("Sprint", isRunning);
    }

    void HandleFootsteps(bool isMoving, bool isRunning)
    {
        if (footstepSource == null) return;

        if (!isMoving)
        {
            footstepSource.Stop();
            footstepSource.clip = null;
            return;
        }

        AudioClip targetClip = isRunning ? runSound : walkSound;

        if (footstepSource.clip != targetClip)
        {
            footstepSource.Stop();
            footstepSource.clip = targetClip;
            footstepSource.loop = true;
            footstepSource.Play();
        }
        else if (!footstepSource.isPlaying)
        {
            footstepSource.Play();
        }

        footstepSource.pitch = isRunning ? 1.3f : 1f;
    }

    // Animation events (safe to keep)
    public void FootStep() { }
    public void RollSound() { }
    public void EndRoll() { }
    public void CantRotate() { }
}
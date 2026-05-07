using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator;

    private NavMeshAgent agent;
    private bool wasMoving = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (animator == null || agent == null) return;

        bool isMoving = agent.velocity.magnitude > 0.1f;

        if (isMoving)
        {
            animator.enabled = true;

            if (!wasMoving)
            {
                animator.Play("Run_Aim", 0, 0f);
            }
        }
        else
        {
            animator.enabled = false;
        }

        wasMoving = isMoving;
    }
}
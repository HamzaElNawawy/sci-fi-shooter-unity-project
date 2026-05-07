using UnityEngine;
using UnityEngine.AI;

public class MovingEnemy : MonoBehaviour
{
    public Transform player;
    public float stoppingDistance = 8f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.stoppingDistance = stoppingDistance;
        }
    }

    void Update()
    {
        if (player == null || agent == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stoppingDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;
            LookAtPlayer();
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
}
using UnityEngine;
using UnityEngine.AI;

public class NPCWander : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    public float walkRadius = 30f;
    public float waitTime = 2f;

    private float waitTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        SetNewDestination();
    }

    void Update()
    {
        // Tell the Animator whether the NPC is walking
        bool walking = agent.velocity.magnitude > 0.1f;

        animator.SetBool("isWalking", walking);

        // Check if NPC has reached its destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                SetNewDestination();
                waitTimer = 0f;
            }
        }
        else
        {
            waitTimer = 0f;
        }
    }

    void SetNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;

        randomDirection += transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(
            randomDirection,
            out hit,
            walkRadius,
            NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}

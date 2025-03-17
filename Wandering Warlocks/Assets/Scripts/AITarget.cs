using UnityEngine;
using UnityEngine.AI;

public class AITarget : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        moveEnemy();
    }

    void moveEnemy()
    {
        agent.SetDestination(target.position);
        bool isMoving = agent.velocity.sqrMagnitude > 0.01f; // Small threshold to avoid jittering
        animator.SetBool("Running", isMoving);
    }
}
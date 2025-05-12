using UnityEngine;
using UnityEngine.AI;

public class AITarget : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    private Animator animator;

    public float detectionRadius = 10f;
    public float damageAmount;
    public float pushForce;

    private Vector3 startPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer <= detectionRadius)
        {
            ChasePlayer();
        }
        else
        {
            ReturnToStart();
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(target.position);
        bool isMoving = agent.velocity.sqrMagnitude > 0.01f;
        animator.SetBool("Running", isMoving);
    }

    void ReturnToStart()
    {
        if (Vector3.Distance(transform.position, startPosition) > 0.1f)
        {
            agent.SetDestination(startPosition);
            bool isMoving = agent.velocity.sqrMagnitude > 0.01f;
            animator.SetBool("Running", isMoving);
        }
        else
        {
            agent.ResetPath();
            animator.SetBool("Running", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyDamageAndPush(collision.gameObject);
        }
    }

    void ApplyDamageAndPush(GameObject player)
{
    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
    if (playerHealth != null)
    {
        playerHealth.TakeDamage(damageAmount);
    }

    Rigidbody playerRb = player.GetComponent<Rigidbody>();
    if (playerRb != null)
    {
        Vector3 pushDirection = (player.transform.position - transform.position);
        pushDirection.y = 0;
        pushDirection = pushDirection.normalized;

        playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);

        Vector3 velocity = playerRb.linearVelocity;
        velocity.y = 0;
        playerRb.linearVelocity = velocity;
    }

    Rigidbody goblinRb = GetComponent<Rigidbody>();
    if (goblinRb != null)
    {
        Vector3 goblinPushDirection = (transform.position - player.transform.position);
        goblinPushDirection.y = 0;
        goblinPushDirection = goblinPushDirection.normalized;

        goblinRb.AddForce(goblinPushDirection * pushForce, ForceMode.Impulse);

        Vector3 goblinVelocity = goblinRb.linearVelocity;
        goblinVelocity.y = 0;
        goblinRb.linearVelocity = goblinVelocity;
    }
}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
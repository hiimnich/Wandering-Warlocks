using UnityEngine;
using UnityEngine.AI;

public class AITarget : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    private Animator animator;

    public float damageAmount;
    public float pushForce;

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
        bool isMoving = agent.velocity.sqrMagnitude > 0.01f;
        animator.SetBool("Running", isMoving);
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
            Debug.Log("Player took damage from Collision");
        }

        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            Vector3 pushDirection = (player.transform.position - transform.position).normalized;
            pushDirection.y = 0;

            playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0, playerRb.linearVelocity.z);
        }

        Rigidbody goblinRb = GetComponent<Rigidbody>();
        if (goblinRb != null)
        {
            Vector3 goblinPushDirection = (transform.position - player.transform.position).normalized;
            goblinPushDirection.y = 0;

            
            goblinRb.AddForce(goblinPushDirection * pushForce, ForceMode.Impulse);
            goblinRb.linearVelocity = new Vector3(goblinRb.linearVelocity.x, 0, goblinRb.linearVelocity.z);
        }
    }
}
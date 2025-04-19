using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 move;
    private Animator animator;
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10;
    public PlayerMana playerMana;
    public float skill1ManaCost = 20f;

    public void onMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        if (playerMana == null)
        {
            playerMana = GetComponent<PlayerMana>();
        }
    }

    void Update()
    {
        movePlayer();
        playRunningAnimation();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerMana != null && playerMana.HasEnoughMana(skill1ManaCost))
            {
                skill1();
                playerMana.UseMana(skill1ManaCost);
            }
        }
    }

    void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (movement.sqrMagnitude > 0.001f)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }
    }

    void playRunningAnimation()
    {
        bool isMoving = move.sqrMagnitude > 0;
        animator.SetBool("Running", isMoving);
        animator.SetBool("Idle", !isMoving);
    }

    void skill1()
    {
        animator.SetBool("Skill1", true);
        StartCoroutine(ResetSkillBoolAfterDelay(0.3f));
        StartCoroutine(FireProjectileAfterDelay(0.4f));
    }

    IEnumerator ResetSkillBoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("Skill1", false);
    }

    IEnumerator FireProjectileAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        projectile.GetComponent<Rigidbody>().linearVelocity = projectileSpawnPoint.forward * projectileSpeed;
    }
}
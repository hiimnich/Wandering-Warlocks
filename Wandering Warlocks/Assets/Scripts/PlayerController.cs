using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 move;
    private Animator animator;

    public void onMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        updateAnimationState();
    }

    void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (movement.sqrMagnitude > 0.001f) // Prevents rotation when stopping
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }
    }

    void updateAnimationState()
    {
        bool isMoving = move.sqrMagnitude > 0;

        //Debug.Log("Before: Running = " + animator.GetBool("Running"));

        animator.SetBool("Running", isMoving);
        animator.SetBool("Idle", !isMoving);

       //Debug.Log("After: Running = " + animator.GetBool("Running"));
    }
}

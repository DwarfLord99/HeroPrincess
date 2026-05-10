using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    [Header("Stats")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;

    private Vector3 velocity;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ApplyGravity();
    }

    public void Move(Vector2 input)
    {
        Vector3 move = new Vector3(input.x, 0, input.y);

        move = transform.TransformDirection(move); // Convert local movement to world space
        velocity.x = move.x * moveSpeed;
        velocity.z = move.z * moveSpeed;

        animator.SetFloat("XSpeed", velocity.x);
        animator.SetFloat("YSpeed", velocity.z);
    }

    void ApplyGravity()
    {
        if (characterController.isGrounded)
        {
            if (velocity.y < 0)
                velocity.y = -2f; // Small negative value to keep the character grounded
            animator.SetBool("IsGrounded", true);
        }
        else
        {
            // Apply gravity when not grounded
            velocity.y += Physics.gravity.y * Time.deltaTime;
            animator.SetBool("IsGrounded", false);
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            animator.SetTrigger("IsJumping");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }
}

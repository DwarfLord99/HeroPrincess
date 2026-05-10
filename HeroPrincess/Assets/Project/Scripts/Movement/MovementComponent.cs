using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    [Header("Stats")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 velocity;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector2 input)
    {
        Vector3 move = new Vector3(input.x, 0, input.y);
        characterController.Move(move * moveSpeed * Time.deltaTime);

        velocity = characterController.velocity;

        animator.SetFloat("XSpeed", velocity.x);
        animator.SetFloat("YSpeed", velocity.z);
        ApplyGravity();
    }

    void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value to keep the character grounded
        }
        else
        {
            // Apply gravity when not grounded
            velocity.y += gravity * Time.deltaTime;
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }
}

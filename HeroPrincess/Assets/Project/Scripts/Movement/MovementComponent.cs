using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    [Header("Stats")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;

    [SerializeField] private LayerMask groundMask;

    private Vector3 velocity;

    public bool isGrounded;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = IsGrounded();
        if (isGrounded )
            Debug.Log("Grounded");

        animator.SetBool("IsGrounded", isGrounded);

        ApplyGravity();

        characterController.Move(velocity * Time.deltaTime);
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
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -10f; // Small negative value to keep the character grounded
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            animator.SetTrigger("IsJumping");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }

    bool IsGrounded()
    {
        float radius = characterController.radius * 0.9f;
        float distance = 0.15f;

        Vector3 origin = transform.position + Vector3.up * (characterController.height / 2f);

        return Physics.SphereCast(
            origin, 
            radius, 
            Vector3.down, 
            out RaycastHit hit, 
            characterController.height / 2f + distance, 
            groundMask, 
            QueryTriggerInteraction.Ignore
            );
    }

    void OnDrawGizmosSelected()
    {
        if (characterController == null)
            return;
        Gizmos.color = Color.red;
        float radius = characterController.radius * 0.9f;
        float distance = 0.15f;
        Vector3 origin = transform.position + Vector3.up * (characterController.height / 2f);
        Gizmos.DrawWireSphere(origin, radius);
        Gizmos.DrawLine(origin, origin + Vector3.down * (characterController.height / 2f + distance));
    }
}

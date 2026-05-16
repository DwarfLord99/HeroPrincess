using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour, IDamageable
{
    private MovementComponent movementComponent;
    private CombatComponent combatComponent;
    private HealthComponent healthComponent;
    private Animator animator;

    [Header("Components")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Transform cameraTransform;

    [Header("Stats")]
    [SerializeField] private float maxHealth;

    [Header("Attacks")]
    [SerializeField] private AbilityData basicAttackData;
    [SerializeField] public AbilityData heavyAttackData;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    private InputAction skill1Action;

    private Vector2 moveInput;

    private bool shouldFaceMoveDirection = false;

    void Start()
    {
        movementComponent = GetComponent<MovementComponent>();
        combatComponent = GetComponent<CombatComponent>();
        healthComponent = GetComponent<HealthComponent>();
        animator = GetComponent<Animator>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");
        skill1Action = InputSystem.actions.FindAction("AttackSkill1");

        maxHealth = healthComponent.GetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveInput.y > 0.1f)
            shouldFaceMoveDirection = true;
        else if (moveInput.y < -0.1f)
            shouldFaceMoveDirection = false;

        HandleCamera();
        HandleMovement();
        movementComponent.Move(moveInput);
        HandleJump();

        if (movementComponent.isGrounded)
            HandleAttack();

        uiManager.SetHealth(healthComponent.GetHealth());
        uiManager.SetHealthValue(healthComponent.GetHealth(), healthComponent.GetMaxHealth());
        uiManager.SetHealthMax(healthComponent.GetMaxHealth());
    }

    void HandleMovement()
    {
        // Read the movement input from the Input System
        moveInput = moveAction.ReadValue<Vector2>();
    }

    void HandleCamera()
    {
        if (cameraTransform != null)
        {
            Vector3 forward = cameraTransform.forward;
            forward.y = 0; // Keep movement on the horizontal plane
            forward.Normalize();

            Vector3 right = cameraTransform.right;
            right.y = 0;
            right.Normalize();

            Vector3 desiredDirection = forward * moveInput.y + right * moveInput.x;

            if (shouldFaceMoveDirection && desiredDirection.sqrMagnitude > 0.01f)
            {
                Quaternion toRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 50f);
            }
        }
    }

    void HandleJump()
    {
        if (jumpAction.triggered)
        {
            movementComponent.Jump();
        }
    }

    void HandleAttack()
    {
        if (attackAction.triggered)
        {
            Debug.Log("Basic Attack triggered");
            animator.SetBool("isBasic", true);

            if (basicAttackData != null)
            {
                Debug.Log("Using basic attack");
            }

            combatComponent.TryUseAbility(basicAttackData);
        }

        if (skill1Action.triggered)
        {
            Debug.Log("Skill 1 triggered");
            animator.SetBool("isHeavy", true);

            if (heavyAttackData != null)
            {
                Debug.Log("Using heavy attack");
            }
            combatComponent.TryUseAbility(heavyAttackData);
        }
    }

    public void TakeDamage(float damage)
    {
        healthComponent.TakeDamage(damage);
        uiManager.SetHealth(healthComponent.GetHealth());
    }
}

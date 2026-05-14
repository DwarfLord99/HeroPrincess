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
        HandleMovement();
        movementComponent.Move(moveInput);
        HandleJump();
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

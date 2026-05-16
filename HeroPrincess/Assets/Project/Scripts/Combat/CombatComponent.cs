using UnityEngine;

public class CombatComponent : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    [SerializeField] private GameObject weaponCollider; // Collider used for detecting hits during attacks
    [SerializeField] private UIManager uiManager;

    [SerializeField] private float currentRage = 0f;
    [SerializeField] private float maxRage = 50f;

    private bool isAttacking = false;
    private AbilityData currentAbility;

    public float GetCurrentRage() => currentRage;
    public float GetMaxRage() => maxRage;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        uiManager.SetRageMax(maxRage);
        uiManager.SetRage(currentRage);
    }

    public bool TryUseAbility(AbilityData ability)
    {
        if (!isAttacking)
        {
            if (currentRage < ability.rageCost)
            {
                Debug.Log("Not enough rage to use ability.");
                return false;
            }

            // Spend rage
            currentRage -= ability.rageCost;
            uiManager.SetRage(currentRage);

            // Begin attack
            isAttacking = true;

            // TODO: Trigger attack animation and apply damage to target
            animator.SetTrigger("Attack");

            // Store the ability being used to apply damage at the correct time in the animation
            currentAbility = ability;

        }

        return true;        
    }

    public void OnAttackHit()
    {
        if (currentAbility == null)
            return;

        // Detect target
        IDamageable target = DetectHitTarget(gameObject);
        if (target == null)
        {
            Debug.Log("No valid target hit.");
            return;
        }
        else
        {
            Debug.Log("Hit target: " + target);
        }

        // Apply damage to target
        target.TakeDamage(currentAbility.damage);

        // Generate rage on hit
        currentRage = Mathf.Clamp(currentRage + currentAbility.rageGenerated, 0, maxRage);
        uiManager.SetRage(currentRage);
    }

    public void EndAttack()
    {
        animator.ResetTrigger("Attack");
        isAttacking = false;
        currentAbility = null;
        animator.SetBool("isBasic", false);
        animator.SetBool("isHeavy", false);
    }

    private IDamageable DetectHitTarget(GameObject attacker)
    {
        if (weaponCollider != null)
        {
            Debug.Log("Detecting hit targets using weapon collider: " + weaponCollider.name);

            Collider[] hitColliders = Physics.OverlapBox(weaponCollider.transform.position, weaponCollider.transform.localScale / 2, weaponCollider.transform.rotation);
            foreach (var hitCollider in hitColliders)
            {
                IDamageable damageable = hitCollider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    if(hitCollider.gameObject == attacker)
                    {
                        Debug.Log("Ignoring self hit on collider: " + hitCollider.name);
                        continue;
                    }

                    return damageable;
                }
                else
                {
                    Debug.Log("No IDamageable component found on hit collider: " + hitCollider.name);
                }
            }
        }

        return null;
    }
}
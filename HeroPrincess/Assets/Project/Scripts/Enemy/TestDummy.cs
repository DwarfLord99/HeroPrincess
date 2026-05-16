using UnityEngine;

public class TestDummy : MonoBehaviour, IDamageable
{
    private HealthComponent healthComponent;
    private DamageNumbers damageNumbers;

    private void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
        damageNumbers = GetComponent<DamageNumbers>();

        Debug.Log($"Test Dummy initialized with {healthComponent.GetHealth()} health.");
    }

    private void Update()
    {
        Debug.Log("Test Dummy health: " + healthComponent.GetHealth());
    }

    public void TakeDamage(float damage)
    {
        healthComponent.TakeDamage(damage);
        damageNumbers.ShowDamage(damage, transform.position + Vector3.up * 2f); // Show damage above the dummy
        Debug.Log($"Test Dummy took {damage} damage. Remaining health: {healthComponent.GetHealth()}");
    }
}

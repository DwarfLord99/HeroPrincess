using UnityEngine;

public class TestDummy : MonoBehaviour, IDamageable
{
    private float health;

    private HealthComponent healthComponent;

    private void Start()
    {
        healthComponent = GetComponent<HealthComponent>();

        health = healthComponent.GetHealth();

        Debug.Log($"Test Dummy initialized with {health} health.");
    }

    private void Update()
    {
        Debug.Log("Test Dummy health: " + health);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Test Dummy took {damage} damage. Remaining health: {health}");
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        healthComponent.Die();
    }
}

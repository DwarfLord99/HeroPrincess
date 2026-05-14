using UnityEngine;

public class TestDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;
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
        Debug.Log("Test Dummy has been destroyed!");
        Destroy(gameObject);
    }
}

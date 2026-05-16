using UnityEngine;
using TMPro;

public class DamageNumbers : MonoBehaviour
{
    [SerializeField] private TextMeshPro damageTextPrefab;

    public void ShowDamage(float damageAmount, Vector3 position)
    {
        TextMeshPro instance = Instantiate(damageTextPrefab, position, Quaternion.identity);
        instance.text = damageAmount.ToString("F0");

        Destroy(instance.gameObject, 1f);
    }
}

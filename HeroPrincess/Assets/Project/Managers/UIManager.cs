using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider rageSlider;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthValue;

    public void SetRageMax(float max)
    {
        rageSlider.maxValue = max;
    }

    public void SetRage(float current)
    {
        rageSlider.value = current;
    }

    public void SetHealthValue(float current, float max)
    {
        healthValue.text = $"{current}/{max}";
    }

    public void SetHealth(float current)
    {
        healthSlider.value = (current);
    }

    public void SetHealthMax(float max)
    {
        healthSlider.maxValue = max;
    }
}

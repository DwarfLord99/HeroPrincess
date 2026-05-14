using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider rageSlider;

    public void SetRageMax(float max)
    {
        rageSlider.maxValue = max;
    }

    public void SetRage(float current)
    {
        rageSlider.value = current;
    }
}

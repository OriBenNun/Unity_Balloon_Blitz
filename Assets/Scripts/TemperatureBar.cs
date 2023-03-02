using UnityEngine;
using UnityEngine.UI;

public class TemperatureBar : MonoBehaviour
{
    private Slider _slider;
    
    [SerializeField] private Image fillImage;
    [SerializeField] private float initialTemperature = 40f;

    private Gradient _temperatureGradient;
    
    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = initialTemperature;
        
        _temperatureGradient = new Gradient();

        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0].color = Color.blue;
        colorKeys[0].time = 0.0f;
        colorKeys[1].color = Color.red;
        colorKeys[1].time = 1.0f;

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1.0f;
        alphaKeys[0].time = 0.0f;
        alphaKeys[1].alpha = 1.0f;
        alphaKeys[1].time = 1.0f;

        _temperatureGradient.SetKeys(colorKeys, alphaKeys);
    }

    public void ChangeCurrentTemperature(float valueToAdd)
    {
        _slider.value += valueToAdd;
        fillImage.color = _temperatureGradient.Evaluate(_slider.normalizedValue);
    }

    public void ResetTemperature() => _slider.value = initialTemperature;
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureBar : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    public void ChangeCurrentTemperature(float valueToAdd)
    {
        _slider.value += valueToAdd;
    }
}

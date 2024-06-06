using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarUI : MonoBehaviour
{

    [SerializeField] private Slider slider;

    public void UpdateHealthBar(float baseHitPoints, float maxHitPoints){
        slider.value = baseHitPoints / maxHitPoints;
    }

    void Update()
    {
        
    }
}

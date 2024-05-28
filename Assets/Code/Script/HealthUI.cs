using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public Image healthBar;
    private float lerpSpeed;

    [Header("References")]
    [SerializeField] TextMeshProUGUI healthBarEnemyUI;

    private void OnGUI(){
        healthBarEnemyUI.text = Health.main.baseHitPoints.ToString();
    }
    private void Update(){
        lerpSpeed = 3f * Time.deltaTime;

        HealthBarFiller();
        colorChanger();
    }

    void HealthBarFiller(){
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, Health.main.baseHitPoints / Health.main.maxHitPoints, lerpSpeed);

    }

    void colorChanger(){
        Color healthColor = Color.Lerp(Color.red, Color.green, (Health.main.baseHitPoints / Health.main.maxHitPoints));

        healthBar.color = healthColor;
    }
}

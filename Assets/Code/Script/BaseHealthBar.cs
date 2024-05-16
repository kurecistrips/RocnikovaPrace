using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BaseHealthBar : MonoBehaviour
{
    public Image healthBar;
    private float lerpSpeed;
    
    [Header("References")]
    [SerializeField] TextMeshProUGUI healthBarUI;

    private void OnGUI(){
        healthBarUI.text = LevelManager.main.BaseHealth.ToString();

    
    }

    private void Update(){
        lerpSpeed = 3f * Time.deltaTime;

        HealthBarFiller();
        colorChanger();
    }

    void HealthBarFiller(){
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, LevelManager.main.BaseHealth / LevelManager.main.maxHealth, lerpSpeed);

    }

    void colorChanger(){
        Color healthColor = Color.Lerp(Color.red, Color.green, (LevelManager.main.BaseHealth / LevelManager.main.maxHealth));

        healthBar.color = healthColor;
    }

    

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] public float baseHitPoints, maxHitPoints = 2;
    [SerializeField] HealthBarUI healthBar;
    
    [SerializeField] private int currencyWorth = 50;

    public static Health main;
    private bool isDestroyed = false;

    private void Awake(){
        main = this;
        healthBar = GetComponentInChildren<HealthBarUI>();
    }
    private void Start(){
        baseHitPoints = maxHitPoints;
        healthBar.UpdateHealthBar(baseHitPoints, maxHitPoints);
    }
    

    public void TakeDamage(float dmg){
        baseHitPoints -= dmg;
        healthBar.UpdateHealthBar(baseHitPoints, maxHitPoints);

        if (baseHitPoints <= 0 && !isDestroyed){
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

}

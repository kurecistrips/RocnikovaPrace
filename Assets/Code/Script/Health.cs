using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] public int maxHitPoints;
    
    [SerializeField] private int currencyWorth = 50;

    public static Health main;
    private bool isDestroyed = false;

    public int baseHitPoints;

    private void Awake(){
        main = this;
    }
    private void Start(){
        baseHitPoints = maxHitPoints;
    }
    

    public void TakeDamage(int dmg){
        baseHitPoints -= dmg;

        if (baseHitPoints <= 0 && !isDestroyed){
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

}

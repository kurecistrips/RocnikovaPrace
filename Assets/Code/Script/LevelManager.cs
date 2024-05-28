using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public float BaseHealth, maxHealth = 100;

    public bool noMoney = false;

    public int currency;

    private bool isNotDead = false;
    
    public float totalTime;

    private void Awake()
    {
        main = this;   
    }

    private void Start(){
        currency = 10000;
        BaseHealth = maxHealth;
        totalTime = 0f;
        
    }
    private void Update(){
        if (BaseHealth <= 0 /*|| EnemySpawner.main.currentWave > EnemySpawner.main.maxWaves*/){
            GameOver();
        }
        totalTime += Time.deltaTime;
        
    }

    public void IncreaseCurrency(int amount){
        currency += amount;
    }

    public bool Spendcurrency(int amount){
        if (amount <= currency){
            currency -= amount;
            noMoney = false;
            return true;
        }
        else{
            noMoney = true;
            return false;
        }
    }

    public void GameOver(){
        if (!isNotDead){
            isNotDead = true;
            
            SceneManager.LoadScene(0);
        }
    }

}

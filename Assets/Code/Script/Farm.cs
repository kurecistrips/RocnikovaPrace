using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farm : MonoBehaviour
{
    [Header("Attribute")]       
    [SerializeField] private int moneyPerWave = 100;
    [SerializeField] private GameObject farmUI;
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private Button destroyBtn;
    [SerializeField] private int baseUpgradeCost = 125;

    private bool placedDuringWave = false;
    private bool giveAway = false;

    private int lvl = 1;
    private int baseMoneyPerWave;

    
    private void Start(){
        if (EnemySpawner.main.isSpawning == true){
            placedDuringWave = true;
            
        }
        destroyBtn.onClick.AddListener(Destroy);
        upgradeBtn.onClick.AddListener(Upgrade);
        baseMoneyPerWave = moneyPerWave;
    }
    private void Update(){
        if (EnemySpawner.main.isSpawning != false){
            
            if (giveAway != true && placedDuringWave != true){
                LevelManager.main.IncreaseCurrency(baseMoneyPerWave);
                giveAway = true;
                
            }
            
        }
        else{
            giveAway = false;
            placedDuringWave = false;
            
        }
        
        
    }

    public void OpenFarmUI(){
        farmUI.SetActive(true);
    }

    public void CloseFarmUI(){
        farmUI.SetActive(false);
    }

    public void Upgrade(){
        if (baseUpgradeCost > LevelManager.main.currency) return;

        if (lvl < 6){
            switch (lvl){
                case 1:
                    LevelManager.main.Spendcurrency(baseUpgradeCost);
                    baseMoneyPerWave = 150;
                    break;
                case 2:
                    LevelManager.main.Spendcurrency(250);
                    baseMoneyPerWave = 400;
                    break;
                case 3:
                    LevelManager.main.Spendcurrency(550);
                    baseMoneyPerWave = 750;
                    break;
                case 4:
                    LevelManager.main.Spendcurrency(1100);
                    baseMoneyPerWave = 1625;
                    break;
                case 5:
                    LevelManager.main.Spendcurrency(3250);
                    baseMoneyPerWave = 2555;
                    break;
            }
            lvl++;
            Debug.Log(lvl);
            Debug.Log(baseMoneyPerWave);
            
        }
        else{
            Debug.Log("Max level reached: " + lvl);
        }
    }

    public void Destroy(){
        Destroy(gameObject);
    }



}

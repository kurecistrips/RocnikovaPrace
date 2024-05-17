using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [Header("Attribute")]       
    [SerializeField] private int moneyPerWave = 100;

    private bool placedDuringWave = false;
    private bool giveAway = false;
    private void Start(){
        if (EnemySpawner.main.isSpawning == true){
            placedDuringWave = true;
        }
    }
    private void Update(){
        if (EnemySpawner.main.isSpawning != false){
            
            if (giveAway != true && placedDuringWave != true){
                LevelManager.main.IncreaseCurrency(moneyPerWave);
                giveAway = true;
                Debug.Log(giveAway);
                Debug.Log(placedDuringWave);
            }
            
        }
        else{
            giveAway = false;
            placedDuringWave = false;
            
        }
        
        
    }

}

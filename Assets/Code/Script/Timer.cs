using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerTxt;
    float timer;
    float timerBeforeWave = 5f;
    void Update(){
        if(EnemySpawner.main.isSpawning != false){
            timer = EnemySpawner.main.waveTimer;
        }
        else{
            timer = timerBeforeWave;
            
        }
        timer -= Time.deltaTime;
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);

        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WaveCounterText : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] TextMeshProUGUI waveCounterUi;

    

    void Update(){
        waveCounterUi.text = EnemySpawner.main.currentWave.ToString();
    }
}

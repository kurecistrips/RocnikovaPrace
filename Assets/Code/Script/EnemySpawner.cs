using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemySpawner : MonoBehaviour
{
    

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributs")]
    [SerializeField] private int baseEnemies = 5;
    [SerializeField] private float enemisePerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemisePerSecondCap = 10f;

    [Header("Events")]
    public static EnemySpawner main;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    public int currentWave = 0;

    //public int maxWaves = 5;

    //private int enemiesAmount;

    //private int count;
    private float baseWaveTimer = 45f;
    public float waveTimer;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; // enemies per sec.
    public bool isSpawning = false;

    private void Awake(){
        onEnemyDestroy.AddListener(EnemyDestroyed);
        main = this;   
    }

    private void Start(){
        StartCoroutine(StartWave());
    }

    private void Update(){
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;
        waveTimer -= Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0){
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0 || waveTimer <= 0){
            EndWave();
            waveTimer = baseWaveTimer;
        }

    }

    private void EnemyDestroyed(){
        enemiesAlive--;
    }

    private IEnumerator StartWave(){
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave() /*enemiesAmount*/;
        eps = EnemiesPerSecond();
        waveTimer = baseWaveTimer;
    }

    private void EndWave(){
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy(/*int enemyIndex, int quantity*/){
        //enemiesAmount = quantity;
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[/*enemyIndex*/index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
  
    }

    private int EnemiesPerWave(){
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond(){
        return Mathf.Clamp(enemisePerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0, enemisePerSecondCap);
    }

    /*private void Waves(){
        for (int i = 0; i < maxWaves; i++){
            if (currentWave == 1){
                SpawnEnemy(0, 5);
            }
            else if (currentWave == 2){
                SpawnEnemy(0, 10);
            }
            else if (currentWave == 3){
                
                SpawnEnemy(1, 6);
            }
            else if (currentWave == 4){
                SpawnEnemy(1, 6);
                SpawnEnemy(0, 10);
            }
            else if (currentWave == 5){
                SpawnEnemy(0, 20);
                SpawnEnemy(1,12);
            }
        }


    }*/
}

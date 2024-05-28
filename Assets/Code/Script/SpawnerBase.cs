using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerBase : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] unitsPrefabs;
    [SerializeField] private Transform endPoint;
    public Transform[] path;

    [Header("Attributes")]
    [SerializeField] private double timeToSpawn = 30f;
    [SerializeField] private int unitsPerSpawn = 1;    

    [Header("Events")]
    public static SpawnerBase main;

    public static UnityEvent onUnitDestroy = new UnityEvent();

    private void SpawnUnit(){
        int index = 0;
        GameObject prefabsToSpawn = unitsPrefabs[index];
        Instantiate(prefabsToSpawn, endPoint.position, Quaternion.identity);
    }
}

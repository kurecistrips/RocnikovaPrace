using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int health = 40;

    private bool isAlive = true;

    public void TakeHit(int dmg){
        health -= dmg;

        if (health <= 0 && !isAlive){
            SpawnerBase.onUnitDestroy.Invoke();
            isAlive = false;
            Destroy(gameObject);
        }
    }
}

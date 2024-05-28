using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 3.2f;
    
    private Transform target;
    private int pathIndex = -1;

    private void Start(){
        target = LevelManager.main.path[pathIndex];
    }

    private void Update(){
        if (Vector2.Distance(target.position, transform.position) <= 0.1f){
            pathIndex--;
            
            if (pathIndex == LevelManager.main.path.Length){
                SpawnerBase.onUnitDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else{
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    
}

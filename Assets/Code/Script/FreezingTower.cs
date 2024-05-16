using System.Collections;
/*using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;*/
using UnityEngine;
using UnityEditor;

public class FreezingTower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float aps = 1f; //attacks per second
    [SerializeField] private float freeteTime = 1f;

    private float timeUntilFire;

    private void Update() {           
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / aps) {
            Freeze();
            timeUntilFire = 0f;
        }

    }

    private void Freeze(){
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
    
        if (hits.Length > 0){
            for(int i = 0; i < hits.Length; i++){
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                    em.UpdateSpeed(0.5f);

                    StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em){
        yield return new WaitForSeconds(freeteTime);

        em.ResetSpeed();
    }

    /*private void OnDrawGizmosSelected(){

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);

    }*/
}

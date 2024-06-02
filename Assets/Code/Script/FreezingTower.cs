using System.Collections;
/*using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;*/
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class FreezingTower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject freezingTwrUI;
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private Button destroyBtn;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float aps = 0.3f; //attacks per second
    [SerializeField] private float freezeTime = 1f;
    [SerializeField] private int baseUpgradeCost = 200;

    public static FreezingTower main;
    private float timeUntilFire;
    private float baseAPS;
    private float baseFreezeTime;
    private float baseTargetingRng;

    public int lvl = 1;

    private void Start(){
        destroyBtn.onClick.AddListener(Destroy);
        upgradeBtn.onClick.AddListener(Upgrade);
        baseAPS = aps;
        baseFreezeTime = freezeTime;
        baseTargetingRng = targetingRange;
    }

    private void Update() {           
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / baseAPS) {
            Freeze();
            timeUntilFire = 0f;
        }

    }

    private void Freeze(){
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, baseTargetingRng, (Vector2)transform.position, 0f, enemyMask);
    
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
        yield return new WaitForSeconds(baseFreezeTime);

        em.ResetSpeed();
    }

    public void OpenFreezTowerUI(){
        freezingTwrUI.SetActive(true);
    }

    public void CloseFreezTowerUI(){
        freezingTwrUI.SetActive(false);
    }

    public void Upgrade(){
        if (baseUpgradeCost > LevelManager.main.currency) return;

        if (lvl < 6){
            switch (lvl){
                case 1:
                    LevelManager.main.Spendcurrency(baseUpgradeCost);
                    baseFreezeTime = 2f;
                    baseTargetingRng = 3.5f;
                    break;
                case 2:
                    LevelManager.main.Spendcurrency(300);
                    baseAPS = 0.6f;
                    baseFreezeTime = 2.5f;
                    break;
                case 3:
                    LevelManager.main.Spendcurrency(425);  
                    baseAPS = 1f;      
                    break;
                case 4:
                    LevelManager.main.Spendcurrency(785);
                    baseTargetingRng = 4.5f;
                    baseAPS = 1.33f;
                    break;
                case 5:
                    LevelManager.main.Spendcurrency(1850);
                    baseTargetingRng = 6f;
                    baseAPS = 1.5f;
                    baseFreezeTime = 3.4f;
                    break;
            }
            lvl++;
            Debug.Log(lvl);
            Debug.Log(baseFreezeTime);
            Debug.Log(baseAPS);
            Debug.Log(baseTargetingRng);

            
        }
        else{
            Debug.Log("Max level reached: " + lvl);
        }
    }

    public void Destroy(){
        Destroy(gameObject);
    }

    /*private void OnDrawGizmosSelected(){

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);

    }*/
}

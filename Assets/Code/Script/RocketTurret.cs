using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.ComponentModel.Design.Serialization;

public class RocketTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject rckTowerUI;
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private Button sellBtn;
    

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float rps = 0.25f; //Rockets Per Second
    [SerializeField] private int baseUpgradeCost = 200;

    public static RocketTurret main;
    private Transform target;
    private float timeUntilFire;

    private float baseRPS;
    private float baseTargetingRng;
    

    public int lvl = 1;


    private void Start(){
        sellBtn.onClick.AddListener(Sell);
        upgradeBtn.onClick.AddListener(Upgrade);
        baseRPS = rps;
        baseTargetingRng = targetingRange;
    }

    private void Update() {
        if (target == null){
            FindTarget();
            return;
        }
        
        RotateTowardsTarget();
        
        if (!CheckTargetIsInRange()) {
            target = null;
        }
        else {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / baseRPS) {
                Shoot();
                timeUntilFire = 0f;
            }
        }

    }

    private void Shoot(){
        GameObject rocketObj = Instantiate(rocketPrefab, firingPoint.position, Quaternion.identity);
        Rocket rocketScript = rocketObj.GetComponent<Rocket>();
        rocketScript.SetTarget(target);
    }

    private bool CheckTargetIsInRange(){
        return Vector2.Distance(target.position, transform.position) <= baseTargetingRng;
    }

    private void RotateTowardsTarget(){
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

    private void FindTarget(){
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, baseTargetingRng, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0){
            target = hits[0].transform;
        }
    }

    public void OpenRocketTowerUI(){
        rckTowerUI.SetActive(true);
    }

    public void CloseRocketTowerUI(){
        rckTowerUI.SetActive(false);
    }

    public void Upgrade(){
        if (baseUpgradeCost > LevelManager.main.currency) return;

        if (lvl < 6){
            switch (lvl){
                case 1:
                    LevelManager.main.Spendcurrency(baseUpgradeCost);
                    baseRPS = 0.55f;
                    break;
                case 2:
                    LevelManager.main.Spendcurrency(250);
                    baseTargetingRng = 6f;
                    break;
                case 3:
                    LevelManager.main.Spendcurrency(800);
                    baseRPS = 0.9f;
                    baseTargetingRng = 7.5f; 
                    break;
                case 4:
                    LevelManager.main.Spendcurrency(1625);
                    baseRPS = 1.35f;
                    baseTargetingRng = 8.7f;
                    break;
                case 5:
                    LevelManager.main.Spendcurrency(3550);
                    baseRPS = 3.5f;
                    baseTargetingRng = 10f;
                    break;
            }
            lvl++;
            Debug.Log(lvl);
            
        }
        else{
            Debug.Log("Max level reached: " + lvl);
        }
    }

    public void Sell(){
        Destroy(gameObject);
    }


    /*private void OnDrawGizmosSelected(){
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);

    }*/

}

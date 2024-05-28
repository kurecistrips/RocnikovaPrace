using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject towerUI;
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private Button sellBtn;
    [SerializeField] private GameObject rangeVis;


    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float bps = 1f; //Bullets Per Second
    [SerializeField] private int baseUpgradeCost = 100;


    private Transform target;

    public static Turret main;

    public int lvl = 1;
    private float baseBps;
    private float baseTargetingRange;

    private float timeUntilFire;

    private void Start(){
        baseBps = bps;
        baseTargetingRange = targetingRange;
        sellBtn.onClick.AddListener(Sell);
        upgradeBtn.onClick.AddListener(Upgrade);
    }

    private void Awake(){
        main = this;
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

            if (timeUntilFire >= 1f / bps) {
                Shoot();
                timeUntilFire = 0f;
            }
        }

    }

    private void Shoot(){
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private bool CheckTargetIsInRange(){
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget(){
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

    private void FindTarget(){
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0){
            target = hits[0].transform;
        }
    }
    
    public void OpenTowerUI(){
        towerUI.SetActive(true);
    }

    public void CloseTowerUI(){
        towerUI.SetActive(false);
    }

    public void Upgrade(){
        if (Cost() > LevelManager.main.currency) return;

        if (lvl < 5){
            LevelManager.main.Spendcurrency(Cost());

            lvl++;

            bps = BPSIncrease();
            targetingRange = RangeIncrease();
            Debug.Log(lvl);
        }
        else{
            Debug.Log("Max level reached: " + lvl);
        }
            
    }

    public void Sell(){
        Destroy(gameObject);
        
    }

    private int Cost(){
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(lvl, 0.6f));
    }
    
    private float BPSIncrease(){
        return baseBps * Mathf.Pow(lvl, 0.5f);
    }

    private float RangeIncrease(){
        return baseTargetingRange * Mathf.Pow(lvl, 0.3f);
    }
    /*private void OnDrawGizmosSelected(){
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);

    }*/

}

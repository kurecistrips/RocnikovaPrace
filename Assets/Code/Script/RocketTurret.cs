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
    [SerializeField] private float rps = 1.5f; //Rockets Per Second

    private Transform target;
    private float timeUntilFire;


    private void Start(){
        sellBtn.onClick.AddListener(Sell);
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

            if (timeUntilFire >= 1f / rps) {
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

    public void OpenRocketTowerUI(){
        rckTowerUI.SetActive(true);
    }

    public void CloseRocketTowerUI(){
        rckTowerUI.SetActive(false);
    }

    public void Upgrade(){

    }

    public void Sell(){
        Destroy(gameObject);
    }


    /*private void OnDrawGizmosSelected(){
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);

    }*/

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
       
    
    private GameObject tower;
    public Turret turret;
    public RocketTurret rckTurret;
    public Farm farm;
    public FreezingTower freezingTwr;
    private Color startColor;


    private void Start() {
        startColor = sr.color;
    }

    private void OnMouseEnter() {
        sr.color = hoverColor;
    }

    private void OnMouseExit() {
        sr.color = startColor;
    }
     
    private void OnMouseDown() {
        if (UIManager.main.IsHovering()) return;

        if (tower != null){
            if (turret != null)
            {
                turret.OpenTowerUI();
            }
            else if (rckTurret != null)
            {
                rckTurret.OpenRocketTowerUI();
            }
            else if (farm != null){
                farm.OpenFarmUI();
            }
            else if (freezingTwr != null){
                freezingTwr.OpenFreezTowerUI();
            }
            return;
        }

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.main.currency){
            Debug.Log("POOR");
            return;
        }

        LevelManager.main.Spendcurrency(towerToBuild.cost);

        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = tower.GetComponent<Turret>();
        rckTurret = tower.GetComponent<RocketTurret>();
        farm = tower.GetComponent<Farm>();
        freezingTwr = tower.GetComponent<FreezingTower>();
    
    }


}

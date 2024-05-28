using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TowerUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouse_over = false;

    [SerializeField] TextMeshProUGUI levelTxtUI;

    private void OnGUI(){
        levelTxtUI.text = "Level: " + Turret.main.lvl.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData){
        mouse_over = true;
        UIManager.main.HoveringState(true);
    }

    public void OnPointerExit(PointerEventData eventData){
        mouse_over = false;
        UIManager.main.HoveringState(false);
        gameObject.SetActive(false);
    }



}

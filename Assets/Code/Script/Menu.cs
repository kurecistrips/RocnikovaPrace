using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;

    public bool mouse_over = false;

    private bool isMenuOpen = true;

    public void ToggleMenu(){
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }
    
    private void OnGUI(){
        currencyUI.text = "Cash: " + LevelManager.main.currency;
    }
    
    //Debuger
    //  |
    //  v
    /*private void Update(){
        if  (mouse_over){
            Debug.Log("Mouse over: " + mouse_over);
        }
    }*/

    public void OnPointerEnter(PointerEventData eventData){
        mouse_over = true;
        UIManager.main.HoveringState(true);
    }

    public void OnPointerExit(PointerEventData eventData){
        mouse_over = false;
        UIManager.main.HoveringState(false);
        
    }

}

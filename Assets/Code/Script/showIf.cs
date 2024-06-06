using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showIf : MonoBehaviour
{
    
    public GameObject text;
    
    public void Show(){
        if (LevelManager.main.noMoney == true){
            text.SetActive(true);
        }
        else{
            text.SetActive(false);
        }
    }
}

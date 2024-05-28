using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager main;

    private bool isHovering;


    private void Awake(){
        main = this;
    }

    public void HoveringState(bool state){
        isHovering = state;
    }

    public bool IsHovering(){
        return isHovering;
    }



}

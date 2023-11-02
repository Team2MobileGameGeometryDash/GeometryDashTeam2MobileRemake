using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoInput : BaseInput
{

    public override void GetInput(PlayerInputManager playerInputManager)
    {
        if (isTouching()) return;
        //Debug.Log("input 1");
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {

            PlayerInputManager.IsTouchEnded = true;
        }
       
    }
}
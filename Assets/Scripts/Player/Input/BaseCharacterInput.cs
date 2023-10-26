using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterInput : BaseInput
{

    public override void GetInput(PlayerInputManager playerInputManager)
    {
        if (isTouching()) return;
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {

            PlayerInputManager.IsTouchEnded = true;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            PlayerInputManager.IsTouchStationary = true;


        }
        else if (touch.phase == TouchPhase.Stationary)
        {
            PlayerInputManager.IsTouchStationary = true;

        }
        else if (touch.phase == TouchPhase.Ended)
        {
            PlayerInputManager.IsTouchStationary = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearModeInput : BaseInput
{
    public override void GetInput(PlayerInputManager playerInputManager)
    {
        if (isTouching()) return;
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {

            PlayerInputManager.IsTouchEnded = true;
        }
    }
}

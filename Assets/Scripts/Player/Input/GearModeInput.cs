using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearModeInput : BaseInput
{
    public override void GetInput(PlayerInputManager playerInputManager)
    {
        if (IsNotTouching()) return;
        Touch touch = Input.GetTouch(0);
        if (IsTopLeft(touch)) return;
        if (touch.phase == TouchPhase.Began)
        {

            PlayerInputManager.IsTouchEnded = true;
        }
    }
}

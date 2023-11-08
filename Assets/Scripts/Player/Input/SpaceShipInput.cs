using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipInput : BaseInput
{

    public override void GetInput(PlayerInputManager playerInputManager)
    {
        if (IsNotTouching()) return;
        Touch touch = Input.GetTouch(0);
        if (IsTopLeft(touch)) return;
        if (touch.phase == TouchPhase.Began)
        {
            playerInputManager.startTimer = true;

        }
        else if (touch.phase == TouchPhase.Moved)
        {
            if (playerInputManager.time <= 0) PlayerInputManager.IsTouchStationary = true;


        }
        else if (touch.phase == TouchPhase.Stationary)
        {
            if (playerInputManager.time <= 0) PlayerInputManager.IsTouchStationary = true;

        }
        else if (touch.phase == TouchPhase.Ended)
        {
            if (!PlayerInputManager.IsTouchStationary)
                PlayerInputManager.IsTouchEnded = true;
            else
                PlayerInputManager.IsTouchEnded = false;

            PlayerInputManager.IsTouchStationary = false;

            playerInputManager.startTimer = false;
            playerInputManager.time = playerInputManager.TimeDelay;
        }
    }
}

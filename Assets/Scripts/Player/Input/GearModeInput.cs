using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearModeInput : BaseInput
{
    public override void GetInput(PlayerInputManager playerInputManager)
    {
        if (isTouching()) return;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInput 
{
    Rect topLeft = new Rect(new Vector2(0, 0), new Vector2(350, Screen.height));
 

    public bool IsTopLeft(Touch touch)
    {
        return (topLeft.Contains(touch.position) && touch.position.y > 1100);
    }

    public bool IsNotTouching() 
    {
        return (Input.touchCount <= 0);
        
    }



    public abstract void GetInput(PlayerInputManager playerInputManager);



}




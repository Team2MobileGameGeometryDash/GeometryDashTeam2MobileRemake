using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInput 
{


    public bool isTouching()
    {
        return (Input.touchCount <= 0);
        
    }



    public abstract void GetInput(PlayerInputManager playerInputManager);
   
   
   



}

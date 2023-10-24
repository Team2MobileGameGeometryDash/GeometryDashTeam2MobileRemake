using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{

    
    public bool IsTouchBegan;
    public bool IsTouchStationary;


    private void Update()
    {
        if (!isTouching()) return;
        InputManager();
    }
    

    
    private bool isTouching()
    {
        if (Input.touchCount <= 0) return false;
        else return true;
    }


    
    private void InputManager()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began) IsTouchBegan = true;
        else if (touch.phase == TouchPhase.Stationary)
        {
            IsTouchStationary = true;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            IsTouchStationary = false;
            IsTouchBegan = false;

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{

    
    public static bool IsTouchEnded;
    public static bool IsTouchStationary;


    public float TimeDelay;
    float time;
    bool startTimer;

    private void Start()
    {
        time = TimeDelay;
    }

    private void Update()
    {

        

        if (!isTouching()) return;
        if (startTimer) Timer();
        InputNew();
    }
    

    
    private bool isTouching()
    {
        if (Input.touchCount <= 0) return false;
        else return true;
    }

    private float Timer()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;

            return time;
        }
        else return time;

    }

    private void InputNew()
    {

        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            startTimer = true;

        }
        else if (touch.phase == TouchPhase.Moved)
        {
            if (time <= 0) IsTouchStationary = true;
            

        }
        else if (touch.phase == TouchPhase.Stationary)
        {
            if (time <= 0) IsTouchStationary = true;
            
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            if(!IsTouchStationary)
                IsTouchEnded = true;

            IsTouchStationary = false;

            startTimer = false;
            time = TimeDelay;
        }
     


    }



}


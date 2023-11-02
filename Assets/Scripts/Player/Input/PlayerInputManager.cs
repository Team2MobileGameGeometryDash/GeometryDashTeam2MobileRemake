using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{

    BaseInput currentInput;

    public static bool IsTouchEnded;
    public static bool IsTouchStationary;


    public float TimeDelay;
    [HideInInspector]
    public float time;
    [HideInInspector]
    public bool startTimer;

    private void Start()
    {
        time = TimeDelay;
        currentInput = new BaseCharacterInput();
        
    }

    private void Update()
    {
        currentInput.GetInput(this);
        if (startTimer) Timer();
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


    private void OnEnable()
    {
        ActionManager.OnChangeShip += OnChangeShip;
    }

    private void OnDisable()
    {
        ActionManager.OnChangeShip -= OnChangeShip;
    }


    private void OnChangeShip(BaseInput newInput)
    {
        currentInput = newInput;
    }



}


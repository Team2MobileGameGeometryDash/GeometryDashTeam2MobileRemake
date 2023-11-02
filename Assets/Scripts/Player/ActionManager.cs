using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionManager 
{

    

    public static Action OnDeath;
    public static Action OnWin;

    public static Action OnUpdateScoreProgress;

    public static Action OnResetCamera;


    public static Action<BaseInput> OnChangeShip;

    #region VFX
    public static Action OnDisableVFX;
    public static Action OnCubeCollision;
    public static Action OnNoCubeCollision;
    #endregion
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
public class VFXManager : MonoBehaviour
{
    public GameObject DeathVFX;
    public GameObject WinVFX;
    public ParticleSystem CubeCollision;

    private void Awake()
    {
        DisactiveDeathVFX();
    }


    private void OnEnable()
    {
        ActionManager.OnDeath += ActiveDeathVFX;
        ActionManager.OnDisableVFX += DisactiveDeathVFX;
        ActionManager.OnWin += ActiveWinVFX;
        ActionManager.OnCubeCollision += ActiveCubeCollisionVFX;
        ActionManager.OnNoCubeCollision += DisactiveCubeCollisionVFX;
    }

    private void ActiveDeathVFX()
    {
        DeathVFX.SetActive(true);
        
    }


    private void DisactiveDeathVFX()
    {
        DeathVFX.SetActive(false);
        WinVFX.SetActive(false);
        
    }


    private void ActiveWinVFX()
    {
        WinVFX.SetActive(true);
    }


    private void ActiveCubeCollisionVFX()
    {
        CubeCollision.Play();
    }

    private void DisactiveCubeCollisionVFX()
    {
        CubeCollision.Stop();
    }

    private void OnDisable()
    {
        ActionManager.OnDeath -= ActiveDeathVFX;
        ActionManager.OnDisableVFX -= DisactiveDeathVFX;
        ActionManager.OnWin -= ActiveWinVFX;
        ActionManager.OnCubeCollision -= ActiveCubeCollisionVFX;
        ActionManager.OnNoCubeCollision -= DisactiveCubeCollisionVFX;
    }

   

   
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
public class VFXManager : MonoBehaviour
{
    public GameObject DeathVFX;
    public GameObject WinVFX;
    public ParticleSystem CubeCollision;
    public GameObject Meteora;

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
        ActionManager.OnMeteoraActiveVFX += ActiveMeteoraVFX;
        ActionManager.OnMeteoraDisactiveVFX += DisactiveMeteoraVFX;
    }


    private void ActiveDeathVFX()
    {
        DeathVFX.SetActive(true);
        
    }

    private void DisactiveDeathVFX()
    {
        DeathVFX.SetActive(false);
        WinVFX.SetActive(false);
        Meteora.SetActive(false);
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

    private void ActiveMeteoraVFX()
    {
        Meteora.SetActive(true);
    }

    private void DisactiveMeteoraVFX()
    {
        Meteora.SetActive(false);
    }


    private void OnDisable()
    {
        ActionManager.OnDeath -= ActiveDeathVFX;
        ActionManager.OnDisableVFX -= DisactiveDeathVFX;
        ActionManager.OnWin -= ActiveWinVFX;
        ActionManager.OnCubeCollision -= ActiveCubeCollisionVFX;
        ActionManager.OnNoCubeCollision -= DisactiveCubeCollisionVFX;
        ActionManager.OnMeteoraActiveVFX -= ActiveMeteoraVFX;
        ActionManager.OnMeteoraDisactiveVFX -= DisactiveMeteoraVFX;
    }

   

   
}

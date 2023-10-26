using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
public class AnimationController : MonoBehaviour
{
    public static Action OnDisableVFX;

    public ParticleSystem DeathParticle1;
    public ParticleSystem DeathParticle2;


    private void Awake()
    {

        DisactiveVFX();
    }


    private void OnEnable()
    {
        PlayerUIManager.OnDeath += ActiveVFX;
        OnDisableVFX += DisactiveVFX;
    }

    private void ActiveVFX()
    {
        DeathParticle1.Play();
        DeathParticle2.Play();
    }


    private void DisactiveVFX()
    {
        DeathParticle1.Stop();
        DeathParticle2.Stop();
    }

    private void OnDisable()
    {
        PlayerUIManager.OnDeath -= ActiveVFX;
        OnDisableVFX -= DisactiveVFX;
    }

   

   
}

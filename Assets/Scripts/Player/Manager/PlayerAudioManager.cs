using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{

    public AudioSource PlayerDeath;
    public AudioSource BackGroundMusic;


    private void OnEnable()
    {
        PlayerUIManager.OnDeath += EnableSoundDeath;
    }

    private void OnDisable()
    {
        PlayerUIManager.OnDeath -= EnableSoundDeath;
    }

    private void EnableSoundDeath()
    {
        PlayerDeath.Play();
        BackGroundMusic.Play();
    }

    


}

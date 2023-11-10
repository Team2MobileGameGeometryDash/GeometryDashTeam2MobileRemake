using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public Slider MasterSlider, MusicSlider, SFXSlider;

    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("MasterVol")) SetStartVolume("MasterVol", MasterSlider);
        else LoadVolume("MasterVol", MasterSlider);
        if (!PlayerPrefs.HasKey("MusicVol")) SetStartVolume("MusicVol", MusicSlider);
        else LoadVolume("MusicVol", MusicSlider);
        if (!PlayerPrefs.HasKey("SFXVol")) SetStartVolume("SFXVol", SFXSlider);
        else LoadVolume("SFXVol", SFXSlider);
    }

    private void SetStartVolume(string key, Slider slider)
    {
        PlayerPrefs.SetFloat(key, 1f);
        slider.value = PlayerPrefs.GetFloat(key);
    }

    private void LoadVolume(string key, Slider slider)
    {
        slider.value = PlayerPrefs.GetFloat(key);
    }

    #region Sliders

    public void MasterVolume()
    {
        float volume = MasterSlider.value;
        AudioMixer.SetFloat("MasterVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVol", volume);
    }
    public void MusicVolume()
    {
        float volume = MusicSlider.value;
        AudioMixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVol", volume);
    }
    public void SFXVolume()
    {
        float volume = SFXSlider.value;
        AudioMixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVol", volume);
    }

    #endregion
}

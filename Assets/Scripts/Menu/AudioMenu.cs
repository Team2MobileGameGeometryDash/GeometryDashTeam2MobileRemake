using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public Slider MasterSlider, MusicSlider, EffectsSlider;

    private void Start()
    {
        SetSliderSettings(MasterSlider, "MasterVolume");
        SetSliderSettings(MusicSlider, "MusicVolume");
        SetSliderSettings(EffectsSlider, "EffectsVolume");
    }

    /// <summary>
    /// Changes values of the audio mixer's groups by using sliders
    /// </summary>
    public void SetVolume()
    {
        SetAudioLevel(MasterSlider, "MasterVolume", "MasterVol");
        SetAudioLevel(MusicSlider, "MusicVolume", "MusicVol");
        SetAudioLevel(EffectsSlider, "EffectsVolume", "EffectsVol");
    }

    /// <summary>
    /// set the slider value to 1
    /// </summary>
    /// <param name="slider"></param>
    /// <param name="key"></param>
    private void SetSliderSettings(Slider slider, string key)
    {
        slider.value = PlayerPrefs.GetFloat(key, 1f);
    }

    /// <summary>
    /// saves the audio slider in the PlayerPrefs and perform the actual volume changing
    /// </summary>
    /// <param name="slider"></param>
    /// <param name="key"> string name used as key for the PlayerPrefs</param>
    /// <param name="groupName">Audio Mixer group name</param>
    private void SetAudioLevel(Slider slider, string key, string groupName)
    {
        AudioMixer.SetFloat(groupName, Mathf.Log10(slider.value) * 20);
        PlayerPrefs.SetFloat(key, slider.value);
    }
}

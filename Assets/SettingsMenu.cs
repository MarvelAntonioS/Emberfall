using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle muteToggle;
    public AudioSource backgroundMusic;

    void Start()
    {
        // Load saved settings
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
        bool isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;

        volumeSlider.value = savedVolume;
        muteToggle.isOn = isMuted;

        ApplyVolume();
    }

    public void ApplyVolume()
    {
        backgroundMusic.volume = muteToggle.isOn ? 0 : volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetInt("Muted", muteToggle.isOn ? 1 : 0);
    }

    public void ToggleMute(bool isMuted)
    {
        backgroundMusic.volume = isMuted ? 0 : volumeSlider.value;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
    }
}

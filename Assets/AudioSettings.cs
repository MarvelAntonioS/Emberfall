using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider volumeSlider;
    public Toggle muteToggle;  // Add this for the mute toggle

    void Start()
    {
        // Set the initial volume to the slider value
        volumeSlider.value = audioSource.volume;
        muteToggle.isOn = audioSource.mute;

        // Listen for slider value changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
        muteToggle.onValueChanged.AddListener(ToggleMute); // Hook up the mute toggle
    }

    public void SetVolume(float volume)
    {
        // Adjust the volume, but only if it's not muted
        if (!audioSource.mute)
        {
            audioSource.volume = volume;
        }
    }

    public void ToggleMute(bool isMuted)
    {
        // Mute or unmute the audio source
        audioSource.mute = isMuted;
    }
}

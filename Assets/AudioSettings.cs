using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider volumeSlider;

    void Start()
    {
        // Set the initial volume to the slider value
        volumeSlider.value = audioSource.volume;

        // Listen for slider value changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // Adjust the volume based on slider value
        audioSource.volume = volume;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        // Start the background music when the game begins
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject gamePanel;
    public GameObject player;
    public Slider volumeSlider;
    public Toggle muteToggle;
    public AudioSource backgroundMusic;

    void Start()
    {
        // Show the main menu at start and hide others
        ShowMainMenu();
        DisablePlayerMovement(true);
        // Load saved settings
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
        bool isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;

        volumeSlider.value = savedVolume;
        muteToggle.isOn = isMuted;
        ApplyVolume();
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        gamePanel.SetActive(false);
        DisablePlayerMovement(true);
    }

    public void ShowSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        DisablePlayerMovement(true);
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        gamePanel.SetActive(true);
        DisablePlayerMovement(false); // Enable player movement when the game starts
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting..."); // This will show in the editor but not in a built game.
    }
    private void DisablePlayerMovement(bool disable)
    {
        // Disable or enable the player's movement script or controller
        player.SetActive(!disable);

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

    public void BackToMainMenu()
    {
        ShowMainMenu();
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // Hide the settings panel
        mainMenuPanel.SetActive(true);  // Show the main menu panel
    }

}

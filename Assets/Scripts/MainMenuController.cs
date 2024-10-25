using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public ThirdPersonMovementSettings playerActionsAsset; // For input system
    private InputAction menuAction;

    void Awake()
    {
        playerActionsAsset = new ThirdPersonMovementSettings();
    }

    void OnEnable()
    {
        menuAction = playerActionsAsset.Player.Menu;
        menuAction.performed += OnMenuInput;
        playerActionsAsset.Player.Enable();
    }

    void OnDisable()
    {
        menuAction.performed -= OnMenuInput;
        playerActionsAsset.Player.Disable();
    }

    void Start()
    {
        ShowMainMenu();
        
        // Load saved settings
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
        bool isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;

        volumeSlider.value = savedVolume;
        muteToggle.isOn = isMuted;
        ApplyVolume();
    }

    private void OnMenuInput(InputAction.CallbackContext context)
    {
        if (PauseMenuCode.isPaused)
        {
            ShowMainMenu();
            Time.timeScale = 0f; // Pause game when main menu is active
        }
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        gamePanel.SetActive(false);
    }

    public void ShowSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        gamePanel.SetActive(true);
        Time.timeScale = 1f; // Resume game on start
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting...");
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
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}

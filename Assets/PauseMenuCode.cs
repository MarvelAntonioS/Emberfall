using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuCode : MonoBehaviour
{
    public GameObject PauseMenu;
    public bool isPaused;
    private ThirdPersonMovementSettings playerActionsAsset;
    private InputAction menu;
   
   
    private void Awake()
    {
        playerActionsAsset = new ThirdPersonMovementSettings();
    }

    private void OnEnable()
    {
        menu = playerActionsAsset.Player.Menu;
        menu.performed += Keypressed;

        Debug.Log("Menu action enabled: " + menu.enabled);

        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        menu.performed -= Keypressed;
        playerActionsAsset.Player.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
    }

    private void Keypressed(InputAction.CallbackContext context)
    {
        var control = context.control;
        Debug.Log("Input was triggered by: " + control.displayName);

        if (control.displayName == "Esc")
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else 
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false; 
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuCode : MonoBehaviour
{
    public GameObject PauseMenu; //new gameobject variable 
    public static bool isPaused; //Boolean variable that can used in other classes in the project
    private ThirdPersonMovementSettings playerActionsAsset; //For the input system 
    private InputAction menu; //new input action variable 
   
   
    //When the game starts 
    private void Awake()
    {
        //Create a new instance of the input system 
        playerActionsAsset = new ThirdPersonMovementSettings();
    }

    //When the object is enabled 
    private void OnEnable()
    {
        //menu can listen to key inputs 
        menu = playerActionsAsset.Player.Menu;

        //Checks if a key input from action menu has been pressed 
        menu.performed += Keypressed;

        //Used for debugging 
        Debug.Log("Menu action enabled: " + menu.enabled);

        playerActionsAsset.Player.Enable();
    }

    //When the object is disabled
    private void OnDisable()
    {
        //Checks if a key input from action menu has been pressed 
        menu.performed -= Keypressed;

        playerActionsAsset.Player.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set the gameobject to false.
        //Essentially, the menu is currently disabled on Start()
        PauseMenu.SetActive(false);
    }

    //This function will change the SetActive of the gameobject 
    //when the key input "esc" has been pressed. 
    private void Keypressed(InputAction.CallbackContext context)
    {
        var control = context.control;
        Debug.Log("Input was triggered by: " + control.displayName);

        if (control.displayName == "Esc")
        {
            if(isPaused)
            {
                ResumeGame(); //If paused is false disable menu 
            }
            else 
            {
                PauseGame(); //If paused is true enable menu 
            }
        }
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f; //stops updates such as animations and physics 
        isPaused = true;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f; //resumes updates 
        isPaused = false; 
    }
}

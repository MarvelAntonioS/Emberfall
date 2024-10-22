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
    public ThirdPersonMovementSettings playerActionsAsset; //For the input system 
    public InputAction menu; //new input action variable 
   
    //When the game starts 
    public void Awake()
    {
        //Create a new instance of the input system 
        playerActionsAsset = new ThirdPersonMovementSettings();
    }

    //When the object is enabled 
    public void OnEnable()
    {
        //menu can listen to key inputs 
        menu = playerActionsAsset.Player.Menu;
        //menu = playerActionsAsset.Player.Move;

        //Checks if a key input from action menu has been pressed 
        menu.performed += Keypressed;

        //Used for debugging 
        Debug.Log("Menu action enabled: " + menu.enabled);

        playerActionsAsset.Player.Enable();
    }

    //When the object is disabled
    public void OnDisable()
    {
        //Checks if a key input from action menu has been pressed 
        menu.performed -= Keypressed;

        playerActionsAsset.Player.Disable();
    }

    // Start is called before the first frame update
    public void Start()
    {
        //Set the gameobject to false.
        //Essentially, the menu is currently disabled on Start()
        PauseMenu.SetActive(false);
        //PauseMenu.SetActive(true); //Incorrect for of unit test 
    }

    //This function will change the SetActive of the gameobject 
    //when the key input "esc" has been pressed. 
    public void Keypressed(InputAction.CallbackContext context)
    {
        var control = context.control;
        Keypressed(control.displayName); // Call the refactored method
    }

    public void Keypressed(string keyName)
    {
        Debug.Log("Input was triggered by: " + keyName);

        //if (keyName == "A")
        if (keyName == "Esc")
        {
            if(isPaused)
            {
                ResumeGame(); // If paused, resume the game
            }
            else 
            {
                PauseGame(); // If not paused, pause the game
            }
        }
}

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f; //stops updates such as animations and physics 
        //Time.timeScale = 1f; //Incorrect code for test
        isPaused = true;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f; //resumes updates 
        isPaused = false; 
    }
}


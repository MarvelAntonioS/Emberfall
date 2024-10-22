using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    //input fields
    private ThirdPersonMovementSettings playerActionsAsset; 
    private InputAction move;
    private InputAction sprint; 
    private bool YesRun; 

    //movement fields
    private Rigidbody rb; //Reference to rigidbody for player 
    [SerializeField] private float movementForce = 1f;

    //Code will be used if we plan to make the player jump 
    //[SerializeField] private float jumpForce = 5f;

    [SerializeField] private float maxSpeed = 5f; //To control the speed of the object 
    private Vector3 forceDirection = Vector3.zero; //Variable used to apply force on an object

    [SerializeField] private Camera playerCamera; //To place the main camera 

    [SerializeField] private Animator animator;
    

    //When the game starts it will get the rigidbody component and initialze it 
    //to the variable rb. Then create a new instance of the Input system as well as
    //get the component of the animator for animation of the object. 
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerActionsAsset = new ThirdPersonMovementSettings();
        animator = this.GetComponent<Animator>();
    }


    //When the object is enabled 
    private void OnEnable()
    {
        //move variable will receive the inputs of the Action "Move" from Action Maps "Player"
        move = playerActionsAsset.Player.Move;

        //variable can be used to listen for "shift" key input
        sprint = playerActionsAsset.Player.Sprinting; 
        sprint.performed += increaseMaxSpeed; 

        //When a key input such as WASD is pressed a function is called
        move.performed += debug;
        sprint.performed += debug; 

        //Enables the Player input action 
        playerActionsAsset.Player.Enable();
    }
    
    //When the object is disabled 
    private void OnDisable()
    {
        //When a key input such as WASD is pressed a function is called
        move.performed -= debug;
        sprint.performed -= debug;

        //Disable the Player input action 
        playerActionsAsset.Player.Disable();
    }

    void Start()
    {
        YesRun = false;
    }

    //Like Update(), but primarily used for physics of Unity. Updates each frame
    private void FixedUpdate()
    {
        physicsMovement(); //Player movement 
        LookAt(); //Camera direction movement 
        
        animator.SetFloat("speed", rb.velocity.magnitude / maxSpeed);
    }

    //This function controls the movement of the object using the Unity's built-in
    //physics engine which is the reason why rigidbody is used. 
    private void physicsMovement()
    {
        //ForceDirecitons value is based on the players input and checks how the x and y axis 
        //are affected in the game as well as the direction of the camera times the preset 
        //movement force applied to the object. 
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        //Add a force to the object to allow the object to move 
        rb.AddForce(forceDirection, ForceMode.Impulse);

        //forceDirection axis values are returned to zero, so that 
        //in the next frame the calculations start from the orign space 
        //and not the last location of the object. 
        forceDirection = Vector3.zero;

        //Controls the gravity on the Y-axis 
        if (rb.velocity.y < 0f)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;

        //Controls the x-axis speed of the object. 
        //Condition states the if the x-axis velocity is greater than the squared maxSpeed
        //we will normalize the x-axis speed to maxSpeed. 
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }

    }

    //Responisble for changing the force applied to the object based on where the 
    //camera is facing
    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }

    //Used for the physics movement to return a normalized foward or right 
    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    //Used for debugging, to ensure that the input system works correctly 
    private void debug(InputAction.CallbackContext context)
    {
        var control = context.control;
        Debug.Log("Input was triggered by: " + control.displayName);
    }

    private void increaseMaxSpeed(InputAction.CallbackContext context)
    {
        var control = context.control;
        if (control.displayName == "Shift")
        {
            if(YesRun == false)
            {
                maxSpeed = 10;
                YesRun = true;
            }
            else
            {
                maxSpeed = 5;
                YesRun = false;
            }
        }
    }
}

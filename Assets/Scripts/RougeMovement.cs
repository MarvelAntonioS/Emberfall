using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueMovement : MonoBehaviour
{
    public float walkSpeed = 2f;  // Speed for walking
    public float runSpeed = 5f;   // Speed for running
    public Animator animator;     // Reference to Animator component

    private Rigidbody rb;         // Reference to Rigidbody component

    void Start()
    {
        // Get the Rigidbody and Animator components from the character
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get input for movement (WASD or arrow keys)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Determine if the player is running by checking if Shift is held down
        bool isRunning = Input.GetKey(KeyCode.LeftShift);  // Hold Shift to run

        // Set the speed based on whether the player is walking or running
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Move the character based on the input and speed
        rb.MovePosition(transform.position + movement.normalized * currentSpeed * Time.deltaTime);

        // Update the Animator's "Speed" parameter based on the movement magnitude
        float movementMagnitude = movement.magnitude; // Calculate how much the character is moving
        animator.SetFloat("Speed", movementMagnitude * currentSpeed); // Set Animator's Speed

        // Rotate the character to face the direction of movement
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement); // Rotate to face the direction
        }
    }
}

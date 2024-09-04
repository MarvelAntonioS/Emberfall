using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    //Inputs fields 
    private ThirdPersonMovementSettings playerActionAsset; 
    private InputAction move; 

    //movement fields 
    private Rigidbody rb;

    [SerializeField]
    private float movementForce = 1f; 

    [SerializeField]
    private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Camera playerCamera; 


    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerActionAsset = new ThirdPersonMovementSettings();
    }

    private void OnEnable()
    {
        move = playerActionAsset.Player.Move;
        playerActionAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionAsset.Player.Disable();
    }

    private void FixedUpdate()
    {
        // forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        // forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector3>()* movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;;

        if (rb.velocity.y < 0f)
        {
            rb.velocity += Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = -2;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }

        //LookAt function 
        LookAt();
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector3>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 foward = playerCamera.transform.forward;
        foward.y = 0;
        return foward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.forward;
        right.y = 0;
        return right.normalized;
    }
}

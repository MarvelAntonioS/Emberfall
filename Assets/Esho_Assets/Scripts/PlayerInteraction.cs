using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 10f;
    public Camera playerCamera; // Reference to the camera
    public AxeSwing axeSwingScript;

    public LayerMask interactableLayer;

    void Update()
    {
        Vector3 rayOrigin = playerCamera.transform.position; 
        Vector3 rayDirection = playerCamera.transform.forward; 

        // Draw the ray in the Scene view to make it visible
        Debug.DrawRay(rayOrigin, rayDirection * interactionDistance, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            TryToChop(rayOrigin, rayDirection);
        }
    }

    void TryToChop(Vector3 rayOrigin, Vector3 rayDirection)
    {
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactionDistance, interactableLayer))
        {
            // Debug log to print where the raycast hits
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name + " at position: " + hit.point);

            Hitable hitableObject = hit.collider.GetComponent<Hitable>();
            if (hitableObject != null)
            {
                hitableObject.Execute();
            }
        }
        else
        {
            // Debug log when the raycast doesn't hit anything
            Debug.Log("Raycast didn't hit anything.");
        }

        if (axeSwingScript != null)
        {
            axeSwingScript.TriggerAxeSwing();
        }
    }
}
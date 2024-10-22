using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 2f;
    public Camera playerCamera; // Reference to the camera
    public AxeSwing axeSwingScript;

    void Update()
    {
        Vector3 rayOrigin = playerCamera.transform.position; 
        Vector3 rayDirection = playerCamera.transform.forward; 


        if (Input.GetMouseButtonDown(0))
        {
            TryToChop(rayOrigin, rayDirection);
        }
    }

    void TryToChop(Vector3 rayOrigin, Vector3 rayDirection)
    {
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactionDistance))
        {

            Hitable hitableObject = hit.collider.GetComponent<Hitable>();
            if (hitableObject != null)
            {
                hitableObject.Execute();
            }
        }

        if (axeSwingScript != null)
        {
            axeSwingScript.TriggerAxeSwing();
        }
    }
}
using UnityEngine; 
using System.Collections.Generic;

public class PlayerPickup : MonoBehaviour
{
    public KeyCode pickupKey = KeyCode.E;
    public float pickupRange = 2f;
    public LayerMask pickupLayer;
    public float pickupWidth = 0.5f;
    public Vector3 pickupBoxOffset = Vector3.zero;
    public bool usePlayerRotation = false;
    public Camera playerCamera; // Reference to player's camera
    private List<Collider> pickupObjects = new List<Collider>();

    private void Update()
    {
        if (Input.GetKeyDown(pickupKey) && pickupObjects.Count > 0)
        {
            PickupObject();
        }
    }

    private void FixedUpdate()
    {
        CheckForPickupObjects();
    }

    private void CheckForPickupObjects()
    {
        pickupObjects.Clear();

        Vector3 boxCenter = transform.position + pickupBoxOffset;
        Vector3 halfExtents = new Vector3(pickupWidth / 2, pickupWidth / 2, pickupRange / 2);

        Quaternion orientation = usePlayerRotation ? playerCamera.transform.rotation : Quaternion.identity;

        Collider[] hits = Physics.OverlapBox(boxCenter, halfExtents, orientation, pickupLayer);
        pickupObjects.AddRange(hits);
    }

    private void PickupObject()
    {
        if (pickupObjects.Count > 0)
        {
            Collider nearestObject = GetNearestObject();
            if (nearestObject != null)
            {
                Debug.Log("Picked up: " + nearestObject.gameObject.name);
                Destroy(nearestObject.gameObject);
            }
        }
    }

    private Collider GetNearestObject()
    {
        Collider nearest = null;
        float minDistance = float.MaxValue;
        Vector3 boxCenter = transform.position + pickupBoxOffset;
        foreach (Collider obj in pickupObjects)
        {
            float distance = Vector3.Distance(boxCenter, obj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = obj;
            }
        }
        return nearest;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 boxCenter = transform.position + pickupBoxOffset;
        Vector3 boxSize = new Vector3(pickupWidth, pickupWidth, pickupRange);

        Quaternion orientation = usePlayerRotation ? (playerCamera ? playerCamera.transform.rotation : Quaternion.identity) : Quaternion.identity;

        Gizmos.matrix = Matrix4x4.TRS(boxCenter, orientation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}

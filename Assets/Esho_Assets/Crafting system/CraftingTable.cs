using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    public GameObject objectToEnable;
    public GameObject player;
    public float interactionRange = 3f;
    private bool isActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed"); // Log a message to indicate key press
        }

        if (Input.GetKeyDown(KeyCode.E) && isActive)
        {
            DeactivateCraftingTable();
        }
        else if (!isActive)
        {
            CheckForInteraction();
        }
    }

    void CheckForInteraction()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionRange && Input.GetKeyDown(KeyCode.E))
        {
            ActivateCraftingTable();
        }
    }

    public void ActivateCraftingTable()
    {
        isActive = true;
        objectToEnable.SetActive(true);
        Cursor.lockState = CursorLockMode.None; // Unlocks the cursor
        Cursor.visible = true;                 // Makes the cursor visible
        SetPlayerControls(false);
    }

    public void DeactivateCraftingTable()
    {
        isActive = false;
        objectToEnable.SetActive(false);
        // Let Unity automatically lock the cursor:
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
        SetPlayerControls(true);
    }

    void SetPlayerControls(bool enable)
    {
        foreach (MonoBehaviour script in player.GetComponents<MonoBehaviour>())
        {
            if (script != this && script != player.GetComponent<Camera>())
            {
                script.enabled = enable;
            }
        }
    }
}
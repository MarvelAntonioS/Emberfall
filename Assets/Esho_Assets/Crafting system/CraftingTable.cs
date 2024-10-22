using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    public GameObject objectToEnable;
    public GameObject player;
    public Camera playerCamera;
    public float interactionRange = 3f;
    private bool isActive = false;

    void Update()
    {
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
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange) && Input.GetKeyDown(KeyCode.E))
        {
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                ActivateCraftingTable();
            }
        }
    }

    void ActivateCraftingTable()
    {
        isActive = true;
        objectToEnable.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SetPlayerControls(false);
    }

    void DeactivateCraftingTable()
    {
        isActive = false;
        objectToEnable.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SetPlayerControls(true);
    }

    void SetPlayerControls(bool enable)
    {
        foreach (Transform child in player.transform)
        {
            if (child.gameObject != playerCamera.gameObject)
            {
                child.gameObject.SetActive(enable);
            }
        }

        foreach (MonoBehaviour script in player.GetComponentsInChildren<MonoBehaviour>())
        {
            if (script.gameObject != playerCamera.gameObject)
            {
                script.enabled = enable;
            }
        }
    }
}
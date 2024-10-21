using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPlacementManager : MonoBehaviour
{
    public Camera mainCamera;  // Main camera for raycasting
    public LayerMask placementMask;  // Mask for valid placement areas
    private GameObject selectedObjectPrefab;  // The prefab to instantiate
    private GameObject currentSelectedObject = null;  // Track the selected object
    private bool isPlacing = false;  // Flag to indicate placement mode
    private bool isMovingObject = false;  // Flag to indicate moving mode

    // Dictionary to store different prefabs
    private Dictionary<string, GameObject> prefabLibrary = new Dictionary<string, GameObject>();

    void Start()
    {
        // Load all prefabs from the "PlaceablePrefabs" folder in Resources
        LoadAllPrefabs();
    }

    void Update()
    {
        // Step 1: Select a prefab from inventory (Example: press '1', '2', '3' to select)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectObject("Cube");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectObject("Table");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectObject("Chair");
        }

        // Step 2: Place the selected object with left-click
        if (isPlacing && Input.GetMouseButtonDown(0) && selectedObjectPrefab != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementMask))
            {
                PlaceObject(hit.point, hit.normal);
                isPlacing = false;  // Exit placement mode
            }
        }

        // Step 3: Select an already placed object with right-click
        if (Input.GetMouseButtonDown(1) && !isPlacing)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("PlaceableObject"))
            {
                currentSelectedObject = hit.collider.gameObject;
                isMovingObject = true;  // Enter moving mode
            }
        }

        // Step 4: Move the selected object with left-click
        if (isMovingObject && Input.GetMouseButtonDown(0) && currentSelectedObject != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementMask))
            {
                currentSelectedObject.transform.position = hit.point;  // Move the object
                isMovingObject = false;  // Exit moving mode
            }
        }

        // Step 5: Remove the selected object with 'R' key
        if (currentSelectedObject != null && Input.GetKeyDown(KeyCode.R))
        {
            Destroy(currentSelectedObject);
            currentSelectedObject = null;  // Clear the selection
        }
    }

    // Method to select a prefab based on name
    void SelectObject(string prefabName)
    {
        if (prefabLibrary.ContainsKey(prefabName))
        {
            selectedObjectPrefab = prefabLibrary[prefabName];
            Debug.Log("Selected object: " + prefabName);
            isPlacing = true;  // Enter placement mode
        }
        else
        {
            Debug.LogError("Prefab not found: " + prefabName);
        }
    }

    // Method to load all prefabs from the "PlaceablePrefabs" folder
    void LoadAllPrefabs()
    {
        GameObject[] loadedPrefabs = Resources.LoadAll<GameObject>("PlaceablePrefabs");

        foreach (GameObject prefab in loadedPrefabs)
        {
            prefabLibrary.Add(prefab.name, prefab);
            Debug.Log("Loaded prefab: " + prefab.name);
        }

        Debug.Log("Total prefabs loaded: " + prefabLibrary.Count);
    }

    // Method to place the selected object
    void PlaceObject(Vector3 position, Vector3 surfaceNormal)
    {
        if (selectedObjectPrefab != null)
        {
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, surfaceNormal);
            GameObject placedObject = Instantiate(selectedObjectPrefab, position, rotation);
            placedObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);  // Adjust size as needed
            placedObject.tag = "PlaceableObject";
            selectedObjectPrefab = null;  // Clear selected prefab
        }
        else
        {
            Debug.LogError("No object selected for placement!");
        }
    }
}

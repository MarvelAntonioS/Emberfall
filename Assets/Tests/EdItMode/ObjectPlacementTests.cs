
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ObjectPlacementTests
{
    private ObjectPlacementManager placementManager;
    private Camera mainCamera;
    private GameObject ground;

    [SetUp]
    public void Setup()
    {
        // Create a temporary GameObject for the PlacementManager
        var managerObject = new GameObject("PlacementManager");
        placementManager = managerObject.AddComponent<ObjectPlacementManager>();

        // Create a temporary Camera for testing
        var cameraObject = new GameObject("TempCamera");
        Camera tempCamera = cameraObject.AddComponent<Camera>();
        placementManager.mainCamera = tempCamera;

        // Position the camera above the plane
        tempCamera.transform.position = new Vector3(0, 10, 0);
        tempCamera.transform.LookAt(Vector3.zero);

        // Set up a simple plane to act as the ground
        var planeObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
        planeObject.transform.position = Vector3.zero;
        planeObject.layer = LayerMask.NameToLayer("ExteriorPlacementArea");

        // Set the placement mask to detect the plane
        placementManager.placementMask = LayerMask.GetMask("ExteriorPlacementArea");

        // Load prefabs using the manager's LoadAllPrefabs method
        placementManager.LoadAllPrefabs();

        // Ensure the "Cube" prefab is loaded
        if (!placementManager.prefabLibrary.ContainsKey("Cube"))
        {
            Assert.Fail("Cube prefab not found in prefabLibrary.");
        }
    }





    [UnityTest]
    public IEnumerator TestObjectPlacement()
    {
        // Select the Cube prefab
        placementManager.SelectObject("Cube");

        // Ensure entering placement mode is successful
        Assert.IsTrue(placementManager.isPlacing);

        // Create a ray from the center of the screen
        Ray ray = placementManager.mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Simulate a raycast hit on the plane
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementManager.placementMask))
        {
            // Call PlaceObject method with hit point
            placementManager.PlaceObject(hit.point, hit.normal);
        }
        else
        {
            Debug.LogError("Raycast did not hit the ground.");
            Assert.Fail("Raycast failed to hit the ground.");
        }

        // Wait a frame to check the results
        yield return null;

        // Check if the object was placed
        GameObject placedObject = GameObject.FindWithTag("PlaceableObject");
        Assert.IsNotNull(placedObject, "The object was not placed successfully.");
    }

}

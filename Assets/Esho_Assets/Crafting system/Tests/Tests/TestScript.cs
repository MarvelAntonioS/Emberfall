using NUnit.Framework;
using UnityEngine;

public class ObjectChoppedSimpleTests
{
    private GameObject testObject;
    private ObjectChopped objectChopped;
    private GameObject axe;

    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject and add the ObjectChopped component
        testObject = new GameObject();
        objectChopped = testObject.AddComponent<ObjectChopped>();

        // Add a collider to the test object
        BoxCollider collider = testObject.AddComponent<BoxCollider>();

        // Set the required tool tag to "Axe"
        objectChopped.GetType().GetField("requiredToolTag", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(objectChopped, "Axe");

        // Create a GameObject representing the axe and set its tag
        axe = new GameObject();
        axe.tag = "Axe"; // Simulating the tool with the required tag
        axe.AddComponent<BoxCollider>(); // Add a collider to the axe

        // Position the axe to intersect with the test object's collider
        axe.transform.position = testObject.transform.position + Vector3.forward; // Slightly offset for intersection
    }

    [Test]
    public void Execute_SetsIsChoppedToTrue_WhenAxeIsUsed()
    {
        // Simulate the axe hitting the test object
        objectChopped.Execute();

        // Use reflection to check if the object is marked as chopped
        bool isChopped = (bool)objectChopped.GetType().GetField("isChopped", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(objectChopped);
        
        // Assert that the object is marked as chopped
        Assert.IsTrue(isChopped, "Object should be marked as chopped after execution.");
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up any leftover objects using DestroyImmediate
        Object.DestroyImmediate(testObject);
        Object.DestroyImmediate(axe); // Make sure to destroy the axe object as well
    }
}

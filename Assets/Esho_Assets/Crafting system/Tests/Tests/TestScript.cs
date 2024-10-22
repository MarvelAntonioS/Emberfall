// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;
// using UnityEngine.UI;
// using System.Collections;
// using NSubstitute;

// public class CraftingTableTests
// {
//     [Test]
//     public void ActivateAndDeactivateCraftingTable_UIAndPlayerControlsToggle()
//     {
//         // Arrange
//         var craftingTable = CreateCraftingTable();
//         var objectToEnable = GameObject.CreatePrimitive(PrimitiveType.Cube);
//         craftingTable.objectToEnable = objectToEnable;
//         craftingTable.playerCamera = new GameObject().AddComponent<Camera>(); // Dummy camera


//         // Act
//         craftingTable.ActivateCraftingTable();

//         // Assert
//         Assert.IsTrue(craftingTable.objectToEnable.activeSelf);
//         Assert.AreEqual(CursorLockMode.None, Cursor.lockState);
//         Assert.IsTrue(Cursor.visible);

//         // Act
//         craftingTable.DeactivateCraftingTable();

//         // Assert
//         Assert.IsFalse(craftingTable.objectToEnable.activeSelf);
//         Assert.AreEqual(CursorLockMode.Locked, Cursor.lockState);
//         Assert.IsFalse(Cursor.visible);
//     }


//     [UnityTest]
//     public IEnumerator CraftingUI_OpensAndClosesSmoothly()
//     {
//         // Arrange
//         var craftingTable = CreateCraftingTable();
//         var objectToEnable = GameObject.CreatePrimitive(PrimitiveType.Cube);
//         craftingTable.objectToEnable = objectToEnable;
//         craftingTable.playerCamera = new GameObject().AddComponent<Camera>(); // Dummy camera

//         // Act - Simulate Interaction (Press E)
//         Input.GetKeyDown(KeyCode.E); 
//         craftingTable.CheckForInteraction(); // Manually call as we can't simulate Raycast in edit mode directly
//         yield return null; // Wait a frame

//         // Assert - UI is open
//         Assert.IsTrue(craftingTable.objectToEnable.activeSelf);


//         // Act - Simulate Closing (Press E again)
//         Input.GetKeyDown(KeyCode.E);
//         yield return null; // Wait a frame for Update()

//         // Assert - UI is closed
//         Assert.IsFalse(craftingTable.objectToEnable.activeSelf);

//     }



//     private CraftingTable CreateCraftingTable()
//     {
//         var go = new GameObject();
//         var craftingTable = go.AddComponent<CraftingTable>();
//         craftingTable.player = new GameObject(); // Dummy player

//         // Mock player components to avoid errors
//         var mockTransform = Substitute.For<Transform>();
//         craftingTable.player.transform.Returns(mockTransform);

//         return craftingTable;
//     }


// }
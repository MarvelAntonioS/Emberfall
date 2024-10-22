using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class CraftingTableTests
{
    private GameObject player;
    private Camera playerCamera;
    private GameObject craftingTable;
    private GameObject craftingUI;
    private CraftingTable craftingTableScript;

    [SetUp]
    public void SetUp()
    {
        // Setup player and camera
        player = new GameObject("Player");
        playerCamera = player.AddComponent<Camera>();

        // Setup crafting table and UI
        craftingTable = new GameObject("CraftingTable");
        craftingTableScript = craftingTable.AddComponent<CraftingTable>();
        craftingUI = new GameObject("CraftingUI");

        craftingTableScript.objectToEnable = craftingUI;
        craftingTableScript.player = player;
        craftingTableScript.playerCamera = playerCamera;

        // Hide UI
        craftingUI.SetActive(false);
    }

    [UnityTest]
    public IEnumerator TestCraftingTableUIOpen()
    {
        // Simulate pressing 'E' to interact
        craftingTableScript.ActivateCraftingTable();

        // Check that the crafting UI becomes active
        yield return null;
        Assert.IsTrue(craftingUI.activeSelf, "Crafting UI should be active after interaction.");
    }

    [UnityTest]
    public IEnumerator TestCraftingTableUIClose()
    {
        craftingTableScript.ActivateCraftingTable(); // Open UI
        yield return null;
        craftingTableScript.DeactivateCraftingTable(); // Close UI

        // Check that the crafting UI is no longer active
        yield return null;
        Assert.IsFalse(craftingUI.activeSelf, "Crafting UI should be inactive after exiting.");
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up objects after each test
        Object.Destroy(player);
        Object.Destroy(craftingTable);
        Object.Destroy(craftingUI);
    }
}

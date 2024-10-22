using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuTest : InputTestFixture
{
    [Test]
    public void Test_PauseMenu_timeScale()
    {
        // Arrange: Set up the input system and PauseMenuCode instance
        var pauseMenu = new GameObject().AddComponent<PauseMenuCode>();
        pauseMenu.PauseMenu = new GameObject();
        
        // Simulate starting conditions
        pauseMenu.Awake();
        pauseMenu.OnEnable();
        pauseMenu.PauseMenu.SetActive(false);
        PauseMenuCode.isPaused = false;

        // Act: Directly call the refactored method with "Esc"
        pauseMenu.Keypressed("Esc");

        // Assert: Check if the timescale has changed
        Assert.AreEqual(0f, Time.timeScale);

        // Clean up
        pauseMenu.OnDisable();
    }

    [Test]
    public void Test_KeyPressed()
    {
        var key = InputSystem.AddDevice<Keyboard>();
        Press(key.escapeKey);
        Assert.AreEqual(true, key.escapeKey.isPressed);
    }

    [Test]
    public void Test_KeyPressed_Causes_Pause()
    {
        // Arrange: Set up the input system and PauseMenuCode instance
        var pauseMenu = new GameObject().AddComponent<PauseMenuCode>();
        pauseMenu.PauseMenu = new GameObject();
        
        // Simulate starting conditions
        pauseMenu.Awake();
        pauseMenu.OnEnable();
        pauseMenu.PauseMenu.SetActive(false);
        PauseMenuCode.isPaused = false;

        // Act: Directly call the refactored method with "Esc"
        pauseMenu.Keypressed("Esc");

        // Assert: Check that the game is paused after pressing the Escape key
        Assert.IsTrue(PauseMenuCode.isPaused);
        Assert.IsTrue(pauseMenu.PauseMenu.activeSelf);

        // Clean up
        pauseMenu.OnDisable();
    }

    public override void Setup()
    {
        base.Setup();
    }
    public override void TearDown()
    {
        // Add teardown code here.
        base.TearDown();
    }
}

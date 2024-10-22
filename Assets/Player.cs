using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public Collider collider1;

    // Start is called before the first frame update   
    void Start()
    {
        // Check if the player has a BoxCollider component attached
        if (!GetComponent<BoxCollider>())
        {
            Debug.LogError("Player object is missing a BoxCollider component. This script requires a BoxCollider for collision detection.");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);   


            // Debug message to indicate collision with GroundItem
            Debug.Log("Player collided with GroundItem: " + item.name);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear();   

    }
}
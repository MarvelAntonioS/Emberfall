using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    // Start is called before the first frame update
    private string playerId = "player1"; // Unique ID for the player


    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }

 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Save to Firebase
        {
            inventory.SaveToFirebase(playerId);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter)) // Load from Firebase
        {
            inventory.LoadFromFirebase(playerId);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear();
    }
}

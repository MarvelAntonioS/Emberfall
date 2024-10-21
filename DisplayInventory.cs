using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DisplayInventory : MonoBehaviour
{

    public GameObject inventoryPrefab; //Prefab for individual inventory items
    public InventoryObject inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;



    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //Create initial display
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        // If the inventory is empty, display an "empty inventory" message
        if (inventory.Container.Items.Count == 0)
        {
            ClearItemsDisplayed();
            ShowEmptyInventoryMessage();
            return;
        }

        // Clear empty message when items are added
        ClearEmptyInventoryMessage();
      

        // Iterate through all items and update the UI
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];

            if (itemsDisplayed.ContainsKey(slot))
            {
                // Update quantity if the item already exists in the display
                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            }
            else
            {
                // Create new UI object for the new item
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");

                // Add the new UI element to the dictionary
                itemsDisplayed.Add(slot, obj);
            }
        }
    }

    public void CreateDisplay()
    {
        // Clear any existing items before creating a new display
        ClearItemsDisplayed();

        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];

            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            itemsDisplayed.Add(slot, obj);
        }
    }

   
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)),
                           Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }

    // Clear the displayed items when inventory is empty or when refreshing the display
    private void ClearItemsDisplayed()
    {
        foreach (var item in itemsDisplayed.Values)
        {
            Destroy(item);
        }
        itemsDisplayed.Clear();
    }

    // Show an empty inventory message
    private void ShowEmptyInventoryMessage()
    {
        // Assuming you have a Text UI element to display the empty message
        GameObject emptyMessage = new GameObject("EmptyMessage");
        TextMeshProUGUI messageText = emptyMessage.AddComponent<TextMeshProUGUI>();
        messageText.text = "Your inventory is empty.";
        messageText.fontSize = 20;
        messageText.alignment = TextAlignmentOptions.Center;
        emptyMessage.transform.SetParent(transform, false); // Set it under the inventory UI

        // Position the message in the center of the screen
        RectTransform rect = emptyMessage.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(600, 100);  // Width and height
        rect.anchoredPosition = new Vector2(0, 0);  // Center the message
    }

    // Clear the empty inventory message when items are added
    private void ClearEmptyInventoryMessage()
    {
        GameObject emptyMessage = GameObject.Find("EmptyMessage");
        if (emptyMessage)
        {
            Destroy(emptyMessage);
        }
    }
}



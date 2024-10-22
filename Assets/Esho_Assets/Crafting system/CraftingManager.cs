using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{

    public Item currentItem;
    public Image customCursor;

    public Slot[] craftingSlots;
    public Slot[] inventorySlots;

    public List<Item> itemList;
    public string[] recipes;
    public Item[] recipeResults;
    public Slot resultSlot;

    private void Update()
{
    if (Input.GetMouseButtonUp(0))
    {
        if (currentItem != null)
        {
            customCursor.gameObject.SetActive(false);
            Slot nearestSlot = null;
            float shortestDistance = float.MaxValue;

            foreach (Slot slot in craftingSlots)
            {
                float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);

                if (dist < shortestDistance)
                {
                    shortestDistance = dist;
                    nearestSlot = slot;
                }
            }

            float maxPlacementDistance = 50f;
            if (shortestDistance <= maxPlacementDistance)
            {
                nearestSlot.gameObject.SetActive(true);
                nearestSlot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                nearestSlot.item = currentItem;
                itemList[nearestSlot.index] = currentItem;
            }

            currentItem = null;
            CheckForCreatedRecipes();
        }
    }
}

    void CheckForCreatedRecipes(){
    resultSlot.gameObject.SetActive(false);
    resultSlot.item = null;

    string currentRecipeString = "";
    foreach(Item item in itemList){
        if(item!= null){
            currentRecipeString += item.itemName;
        }else{
            currentRecipeString += "null";
        }
    }

    for (int i = 0; i < recipes.Length; i++)
    {
        if(recipes[i] == currentRecipeString)
        {
            
                resultSlot.gameObject.SetActive(true);
                resultSlot.GetComponent<Image>().sprite = recipeResults[i].GetComponent<Image>().sprite;
                resultSlot.item = recipeResults[i];
        }
    }
    }


public void OnClickSlot(Slot slot) 
{
    if (slot == resultSlot && resultSlot.item != null)
    {
        TransferToInventory(resultSlot.item); // Transfer to inventory
        resultSlot.item = null;
        resultSlot.gameObject.SetActive(false); 
        ClearCraftingGrid(); // Clear crafting slots after
    }
    else
    {
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCreatedRecipes();
    }
}

void ClearCraftingGrid()
{
    foreach (Slot slot in craftingSlots)
    {
        slot.item = null;

        Image imageComponent = slot.GetComponent<Image>();
        if (imageComponent != null)
        {
            imageComponent.sprite = null;
        }

        slot.gameObject.SetActive(false);
    }

    for (int i = 0; i < itemList.Count; i++)
    {
        itemList[i] = null;
    }
}


    public void MouseDownItem(Item item){
        if (currentItem == null){
            currentItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;

        }

    }

    public void TransferToInventory(Item craftedItem)
{
    foreach (Slot slot in inventorySlots)
    {
        if (slot.item == null) 
        {
            slot.item = craftedItem;
            slot.GetComponent<Image>().sprite = craftedItem.GetComponent<Image>().sprite;
            slot.gameObject.SetActive(true);
            break; 
        }
    }
}
    
}

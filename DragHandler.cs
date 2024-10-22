using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject objectPrefab; // Prefab of the object to be placed in the game world
    private GameObject draggingIcon; // The icon the player drags
    private RectTransform draggingPlane; // The UI plane on which the icon is dragged
    private Canvas canvas; // The Canvas that contains the drag-and-drop UI
    public InventoryObject inventory; // Reference to the inventory system

    private Item currentItem; // The item being dragged

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>(); // Get the canvas for drag context
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Create the icon when the drag starts
        draggingIcon = new GameObject("DraggingIcon");
        draggingIcon.transform.SetParent(canvas.transform, false);
        draggingIcon.transform.SetAsLastSibling();

        // Add Image component to represent the object
        var image = draggingIcon.AddComponent<Image>();
        image.sprite = GetComponent<Image>().sprite; // Use the same sprite as the UI element
        image.SetNativeSize();

        draggingPlane = canvas.transform as RectTransform;

        // Get the current item from the inventory
        currentItem = GetComponent<ItemButton>().item; // Assuming ItemButton is your UI button for items
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the position of the dragged icon
        if (draggingIcon != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(draggingPlane, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
            draggingIcon.GetComponent<RectTransform>().localPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Drop the object into the game world when dragging ends
        if (draggingIcon != null)
        {
            // Cast a ray from the camera to the mouse position to find a valid place
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Instantiate the object prefab at the hit position
                GameObject placedObject = Instantiate(objectPrefab, hit.point, Quaternion.identity);

                // Remove the item from the inventory
                inventory.RemoveItem(currentItem);
                FindObjectOfType<DisplayInventory>().UpdateDisplay(); // Update the inventory display to remove the item
            }
        }

        // Destroy the dragging icon
        Destroy(draggingIcon);
    }
}


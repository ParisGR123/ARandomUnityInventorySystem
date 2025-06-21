using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    
    [Header("UI")]
    public Image image; // Reference to the UI Image component for visual representation
    public TextMeshProUGUI countText;


    public Item item; // Reference to the Item scriptable object that this draggable item represents
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag; // To store the parent transform before dragging

   
    public void InitializeItem(Item newItem)
    {
        item = newItem; // Assign the new item to the script
        image.sprite = newItem.image;
        RefreshCount(); // Update the count text to reflect the initial count
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1; // Only show count text if count is greater than 1
        countText.gameObject.SetActive(textActive); // Set the active state of the count text based on the count value

    }







    public void OnBeginDrag(PointerEventData eventData)
    {
        // Logic to handle the beginning of a drag operation
        Debug.Log("Drag started for: " + gameObject.name);

        image.raycastTarget = false; // Disable raycast target to allow interaction with other UI elements during drag

        parentAfterDrag = transform.parent; // Store the current parent
        transform.SetParent(transform.root); // Change the parent to the root to avoid hierarchy issues during drag
        transform.SetAsLastSibling(); // Bring the item to the front in the hierarchy

    }

    public void OnDrag(PointerEventData eventData)
    {
        // Logic to handle the dragging of the item
        Debug.Log("Dragging: " + gameObject.name + " to position: " + eventData.position);
        transform.position = eventData.position; // Move the item to the current mouse position

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Logic to handle the end of a drag operation
        Debug.Log("Drag ended for: " + gameObject.name + " at position: " + eventData.position);
        transform.SetParent(parentAfterDrag); // Reset the parent to the original one

        image.raycastTarget = true; // Re-enable raycast target for the image
    }
}




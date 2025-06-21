using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image; // Reference to the UI Image component for visual representation
    public Color selectedColor, unSelectedColor; // Colors for selected and unselected states

    


    private void Awake()
    {
        unSelectedColor = image.color; // Store the initial color as unselected color
    }

    public void Select()
    {
        image.color = selectedColor; // Change the color to indicate selection
    }

    public void DeSelect()
    {
               image.color = unSelectedColor; // Change the color back to indicate deselection
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform; // Set the parent of the draggable item to this slot
        }
        else
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

            GameObject current = transform.GetChild(0).gameObject;
            DraggableItem currentDraggable = current.GetComponent<DraggableItem>();

            currentDraggable.transform.SetParent(draggableItem.parentAfterDrag);
            draggableItem.parentAfterDrag = transform;
        }
    }
}

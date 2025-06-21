using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots; // Array of inventory slots
    public GameObject inventoryItemPrefab; // Prefab for the draggable item

    public int SelectedSlot = -1; // Index of the currently selected slot, -1 means no slot is selected

    private void Start()
    {
        ChangeSelectedSlot(0); // Initialize the first slot as selected
    }

    private void Update()
    {
        // Check keys 1-9 (Key.digit1 to Key.digit9) and 0 for the 10th slot
        for (int i = 0; i < 10; i++)
        {
            Key keyToCheck = (i < 9) ? (Key)((int)Key.Digit1 + i) : Key.Digit0;
            if (Keyboard.current[keyToCheck].wasPressedThisFrame)
            {
                if (i < inventorySlots.Length)
                {
                    ChangeSelectedSlot(i);
                }
            }
        }

        // Add scroll wheel support for changing selected slot
        if (Mouse.current != null)
        {
            float scroll = Mouse.current.scroll.y.ReadValue();
            if (scroll > 0.1f)
            {
                int nextSlot = (SelectedSlot - 1 + Mathf.Min(inventorySlots.Length, 10)) % Mathf.Min(inventorySlots.Length, 10);
                ChangeSelectedSlot(nextSlot);
            }
            else if (scroll < -0.1f)
            {
                int nextSlot = (SelectedSlot + 1) % Mathf.Min(inventorySlots.Length, 10);
                ChangeSelectedSlot(nextSlot);
            }
        }
    }

    public void ChangeSelectedSlot(int newValue)
    {
        if (SelectedSlot >= 0 && SelectedSlot < inventorySlots.Length)
        {
            inventorySlots[SelectedSlot].DeSelect(); // Deselect the previously selected slot
        }
        inventorySlots[newValue].Select(); // Select the new slot
        SelectedSlot = newValue; // Update the selected slot index

    }

    public bool AddItem(Item item)
    {
        //Find empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < item.MaxStackSize)
            {
                itemInSlot.count++; // Increment the count of the item in the slot
                itemInSlot.RefreshCount(); // Refresh the count text to reflect the new count
                return true; // Exit after adding the item to an existing slot
            }
        }

        //Find empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
            if (itemInSlot == null) // If the slot is empty
            {
                SpawnNewItem(item, slot);
                return true; // Exit after adding the item
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform); // Assuming you have a prefab for the draggable item
        DraggableItem inventoryItem = newItemGo.GetComponent<DraggableItem>();
        inventoryItem.InitializeItem(item); // Initialize the item with the scriptable object

    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[SelectedSlot];
        DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--; // Decrement the count of the item in the slot
                if(itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject); // Destroy the item if count is zero
                }
                else
                {
                    itemInSlot.RefreshCount(); // Refresh the count text to reflect the new count
                }
            }
            return item; // Return the item in the selected slot
        }
         return null; // Return null if no item is found in the selected slot

    }
}

using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager; // Reference to the InventoryManager script
    public Item[] items; // Array of items to be added to the inventory


   public void spawnitem(int id)
    {
        inventoryManager.AddItem(items[id]); // Add the item to the inventory using its ID
    }

    public void GetSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false); // Get the currently selected item from the inventory
        if (receivedItem != null)
        {
            Debug.Log("Selected Item: " + receivedItem.name); // Log the name of the selected item
        }
        else
        {
            Debug.Log("No item selected."); // Log if no item is selected
        }
    }
    public void UseSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(true); // Get the currently selected item from the inventory
        if (receivedItem != null)
        {
            Debug.Log("Used Item: " + receivedItem.name); // Log the name of the selected item
        }
        else
        {
            Debug.Log("No item selected."); // Log if no item is selected
        }
    }
}

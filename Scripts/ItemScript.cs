using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [Header("Only Gameplay")]
    public TileBase tile; // Tile to be used in the Tilemap
    public ItemType type; // Type of the item
    public actionType action; // Action associated with the item
    public Vector2Int range = new Vector2Int(5, 4); // Range of the item in the Tilemap

    [Header("Only UI")]
    public bool isStackable = true; // Whether the item can be stacked in the inventory

    [Header("Both")]
    public Sprite image; // Image to be used in the UI and in game
    public int MaxStackSize = 64; // Maximum stack size for the item


}

public enum ItemType
{
    BuildingBlock,
    Tool,
}

public enum actionType
{
   Dig,
   Mine,
}

using UnityEngine;

// This creates a new object that can be created in the unity editor. We can use this as a blueprint for items in our game.
// To create an item, right click in the Project window -> Create -> Inventory -> Item. This now creates an Item asset.
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    // ----------- Public variables ------------ //
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public ItemType itemType;
    public GameObject mesh;
    // ----------------------------------------- //

    // ----------- Virtual functions ------------ //
    // virtual functions are functions that can be overrided in classes that inherit the Item class
    public virtual void Use() { }  
    public virtual void Use(GameObject player) { }
    // ------------------------------------------ //
}

public enum ItemType { Consumable, Equipment, Resource }

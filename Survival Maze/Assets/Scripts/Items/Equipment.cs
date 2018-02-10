using UnityEngine;

// This is a blueprint for the Equipment item. It inherits from the Item class. CreateAssetMenu creates a new
// object that can be created when right clicking on the Project window. Right click on 
// Project window -> Create -> Inventory -> Equipment
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    // ---------- Public variables ---------- //
    public EquipmentSlotIndex equipSlot; // Six types of equipment as seen at the bottom
    public float damageValue;
    public float armorValue;
    // -------------------------------------- //

    // ------------ Overriding the Item Use() function ------------ //
    public override void Use(GameObject player)
    {
        base.Use();

        if (player.GetComponent<EquippedController>().Equip(this)) // if equipping this item is successful, remove the equipment from the inventory
        {
            player.GetComponent<InventoryController>().Remove(this);
        }
    }
    // ------------------------------------------------------------ //
}

public enum EquipmentSlotIndex { Head, Chest, Legs, Weapon, Shield, Feet }

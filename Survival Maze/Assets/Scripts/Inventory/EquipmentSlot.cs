using UnityEngine;
using UnityEngine.UI;

// This class is attached to the Equipment Slots. The purpose of this script is to control all functionality in the equipment slots. This includes
// keeping a reference to what item is in the slot, adding an item to the slot, removing an item from the slot, and clearing all other slots that
// are not currently filled with an item
public class EquipmentSlot : MonoBehaviour {

    // ------- Public variables ------- //
    public Image icon;
    public Button removeButton;
    // -------------------------------- //

    // ------- Private variables ------- //
    private Equipment equipment;
    // --------------------------------- //

    // ------- Testing variables (to be removed later) ------- //
    public Text text;
    // ------------------------------------------------------- //

    // --------------- Adding a new equipment item to the slot --------------- //
    public void AddEquipment(Equipment newEquipment)
    {
        equipment = newEquipment; // keeping the reference to the new item

        // -------- Setting up taken slot -------- //
        icon.sprite = equipment.icon;
        icon.enabled = true;

        removeButton.interactable = true;

        text.enabled = true; // testing
        text.text = equipment.name; // testing
        // --------------------------------------- //
    }
    // ----------------------------------------------------------------------- //

    // --------------- Setting a slot to a clean state --------------- //
    public void ClearSlot()
    {
        equipment = null; // getting rid of reference to the item

        // --------- Clearing the slot ---------- //
        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;

        text.enabled = false; // testing
        text.text = ""; // testing
        // -------------------------------------- //
    }
    // ---------------------------------------------------------------- //

    // ------- When remove button is pressed on equipment slot ------- //
    public void OnRemoveButton()
    {
        InventoryController.instance.Add(equipment); // add the item back to the inventory
        EquippedController.instance.Unequip(equipment); // remove it from the list of equipped items
    }
    // --------------------------------------------------------------- //
}

using UnityEngine;
using UnityEngine.UI;

// This script is on the InventorySlot game objects
// Purpose: This script provides all the functionality on a single slot. This includes adding a new item to the slot, clearing a slot if it's empty,
//          removing an item from a slot if the remove button is pressed, and use the item using the items Use() function.
public class InventorySlot : MonoBehaviour {

    // --------- Public variables --------- //
    public Image icon;
    public Button removeButton;
    public GameObject stackPanel;
    public Text stackCountText;
    public int stackCount = 0;
    // ------------------------------------ //

    // --------- Private variables --------- //
    private Item item; // reference to what item is in that slot
    // ------------------------------------- //

    // ----------- Testing variables ----------- //
    public Text text;
    // ----------------------------------------- //

    // ----------- Getter functions ----------- //
    public Item GetItem() { return item; }
    // ---------------------------------------- //

    // ----------- Adding an item to the slot ----------- //
    public void AddItem(Item newItem) // Adds item to the slot
    {
        item = newItem; // getting a reference to new item

        // ---------- Setting up slot ----------- //
        icon.sprite = item.icon;
        icon.enabled = true;

        removeButton.interactable = true;

        stackPanel.SetActive(false); // making sure the stacking feature is turned off (if item is a Resource it will turn back on in if statement)
        stackCountText.text = "";

        if (newItem.itemType == ItemType.Resource) // if the item is also a resource, enable the stacking feature
        {
            stackPanel.SetActive(true);

            Resource tempResource = (Resource)item;
            stackCountText.text = tempResource.stackAmount.ToString();
        }
        // -------------------------------------- //

        text.enabled = true; // testing
        text.text = item.name; // testing
    }
    // -------------------------------------------------- //

    // ----------- Clearing an empty slot ----------- //
    public void ClearSlot() // Clears slot fully
    {
        item = null; // removing reference to the item

        // ------------ Clearing the slot ------------- //
        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;

        stackPanel.SetActive(false);
        stackCountText.text = "";
        stackCount = 0;

        text.enabled = false; // testing
        text.text = ""; // testing
        // -------------------------------------------- //
    }
    // ---------------------------------------------- //

    // ----------- Remove button functionality ----------- //
    public void OnRemoveButton()
    {
        Instantiate(item.mesh, InventoryController.instance.transform.position, InventoryController.instance.transform.rotation); // drops item on the ground where the player is (test for now)
        InventoryController.instance.Remove(item); // Removes item from the inventory list (found in the Inventory script)
    }
    // --------------------------------------------------- //

    // ----------- Using the item functionality ----------- //
    public void UseItem()
    {
        if (item != null)
        {
            if (item.itemType == ItemType.Consumable)
            {
                Consumable consumable = (Consumable)item;
                consumable.Use(InventoryController.instance.gameObject);
            }
            else if (item.itemType == ItemType.Equipment)
            {
                Equipment equipment = (Equipment)item;
                equipment.Use();
            }
            // Resource items don't have an if statment because you can't use Resource items, they are just meant for crafting
        }
    }
    // ---------------------------------------------------- //

}

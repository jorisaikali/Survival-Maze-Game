using System.Linq;
using UnityEngine;

// This script is on the Canvas game object
// Purpose: This script deals with all UI related functionality for the inventory. This includes opening the inventory when the player presses the tab key and updating the UI
//          when something changes in the inventory.
public class InventoryUI : MonoBehaviour {

    // --------- Public variables --------- //
    public Transform itemsParent;
    public GameObject inventoryUI;
    // ------------------------------------ //

    // --------- Private variables --------- //
    private InventoryController inventory;
    private InventorySlot[] slots;
    private PlayerMovement playerMovement;
    // ------------------------------------- //

    void Start () {
        inventory = InventoryController.instance; // getting the singleton instance of InventoryController
        inventory.onItemChangedCallback += UpdateUI; // subscribing UpdateUI() to onItemChangedCallback event
        inventory.onResourceChangedCallback += ConvertAndUpdateResourceUI; // subscribing ConvertAndUpdateResourceUI to onResourceChangedCallback event

        slots = itemsParent.GetComponentsInChildren<InventorySlot>(); // getting a reference to all slots

        playerMovement = gameObject.transform.parent.GetComponent<PlayerMovement>();
    }
	
	void Update () {
		if (Input.GetButtonDown("Inventory")) // if tab is pressed
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf); // if inventory open->close, if closed->open

            if (Cursor.lockState == CursorLockMode.Locked) // if cursor is locked, unlock it, if it's unlocked, lock it
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            
            if (playerMovement.GetLockRotation()) // if player rotation is locked, unlock it, if unlocked, lock it
            {
                playerMovement.UnlockRotation();
            }
            else
            {
                playerMovement.LockRotation();
            }

            if (Cursor.visible) // if cursor is visible, make it invisible, if invisible, make it visible
            {
                Cursor.visible = false;
            }
            else
            {
                Cursor.visible = true;
            }
        }
	}

    // ---------- Convertting Item -> Resource ---------- //
    private void ConvertAndUpdateResourceUI(Item item)
    {
        UpdateResourceUI((Resource)item); // Convert Item to Resource and pass it to UpdateResourceUI
    }
    // -------------------------------------------------- //

    // ---------- Updating the resource UI ----------- //
    private void UpdateResourceUI(Resource item)
    {
        for (int i = 0; i < slots.Length; i++) // iterate through all slots
        {
            if (slots[i].GetItem().name == item.name) // if slots[i] items name equals the new items name
            {
                slots[i].stackCount++; // increase the count because if in this if statement it means the Resource item is already in the players inventory
                item.stackAmount++; // increase the stackAmount on the actual item too because if the item is dropped and picked up again it needs to have the same stackAmount

                inventory.items = inventory.items.Distinct(new DistinctItemComparer()).ToList(); // Removes duplicate resources in the inventory
                UpdateUI();

                return;
            }
        }
    }
    // ----------------------------------------------- //

    // ----------------------- Updating the inventory UI ----------------------- //
    // This happens whenever the inventory changes somehow
    private void UpdateUI()
    {
        inventory.items = inventory.items.Distinct(new DistinctItemComparer()).ToList(); // Removes duplicate resources in the inventory

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count) // if i < # of items in the inventory list (found in Inventory script)
            {
                slots[i].AddItem(inventory.items[i]); // calling AddItem from InventorySlot, adding the item to the slot
            }
            else
            {
                slots[i].ClearSlot(); // Clear the rest of the slots
            }
        }
    }
    // ------------------------------------------------------------------------- //
}

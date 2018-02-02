using UnityEngine;

// This script is attached to the Canvas game object. The purpose of this script is to deal with all equipment UI related functionality. This includes
// keeping a reference to all slots in the equipment panel, updating the equipment UI when an item is changed, and showing the equipment panel when
// tab is pressed.
public class EquipmentUI : MonoBehaviour {

    // --------- Public variables --------- //
    public Transform equippedParent;
    public GameObject equippedUI;
    // ------------------------------------ //

    // --------- Private variables --------- //
    private EquippedController equippedController;
    private EquipmentSlot[] slots;
    // ------------------------------------- //

    private void Start()
    {
        equippedController = EquippedController.instance; // getting a reference to EquippedController singleton
        equippedController.onEquippedChangedCallback += UpdateUI; // subscribing UpdateUI() to the onEquippedChangedCallback

        slots = equippedParent.GetComponentsInChildren<EquipmentSlot>(); // getting all slots in the equipment panel
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory")) // if tab is pressed
        {
            equippedUI.SetActive(!equippedUI.activeSelf); // if equipped open->close, if closed->open
        }
    }

    // ----------------- Subscribed function to update UI ----------------- //
    /* This function is subscribed to the onEquippedChangeCallback delegate. This means that whenever
       onEquippedChangedCallback.Invoke() is called, this function will be ran, updating the equipmentUI */
    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++) // loop through all slots in equipment panel
        {
            if (equippedController.currentEquipment[i] != null) // if currentEquipment[i] (array in EquippedController) has an item in it, add that equipment item to the ith slot 
            {
                slots[i].AddEquipment(equippedController.currentEquipment[i]);
            }
            else // if currentEquipment[i] is empty, clear the slot
            {
                slots[i].ClearSlot();
            }
        }
    }
    // -------------------------------------------------------------------- //
}

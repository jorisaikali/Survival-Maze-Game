using UnityEngine;

// This script is attached to the Player game object. The purpose of this script is to implement the rules and core functionality of the equipped equipment items. 
// This includes limiting the space in the equipped array, keeping track of equipment items currently in the equipped array, adding/removing items from the equipped array, 
// and creating the core for updating the equipped UI.
public class EquippedController : MonoBehaviour {

    // ----------------- Making EquippedController a singleton ----------------- //
    // A singleton is a class that is always "alive". Rather than making an instance of it to call it,
    // you can call it as if it were static, EquippedController.instance
    /*
    #region Singleton

    public static EquippedController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Equipment found");
            return;
        }

        instance = this;
    }

    #endregion
    */
    // ------------------------------------------------------------------------- //

    // ----------- Public variables ----------- //
    public int space = 5;
    public Equipment[] currentEquipment;

    // A delegate is a variable that can have functions subscribed to it and when the delegate is invoked 
    // (Invoke()), it will run all subscribed functions
    public delegate void OnEquippedChanged(); // creating the delegate OnEquippedChanged
    public OnEquippedChanged onEquippedChangedCallback; // attaching the callback variable to the delegate
    // ----------------------------------------- //

    // ----------- Private variables ----------- //
    private int numSlots;
    // ----------------------------------------- //

    private void Start()
    {
        numSlots = System.Enum.GetNames(typeof(EquipmentSlotIndex)).Length; // getting the length of EquipmentSlotIndex enum
        currentEquipment = new Equipment[numSlots];
    }

    // ------- Getting how many equipment items are currently in currentEquipment ------- //
    public int EquippedGetCount()
    {
        int count = 0;

        for (int i = 0; i < currentEquipment.Length; i++) // iterate through currentEquipment and increment count whenever currentEquipment[i] is not null
        {
            if (currentEquipment[i] != null)
            {
                count += 1;
            }
        }

        return count;
    }
    // ---------------------------------------------------------------------------------- //

    // ---------------- Adding an equipment item to currentEquipped ---------------- //
    public bool Equip(Equipment newItem)
    {
        if (EquippedGetCount() >= space) // checking if equipped limit has been reached, if so don't add equipment
        {
            Debug.Log("Not enough room");
            return false;
        }

        // getting the slot index where the equipment should be added. If you hover your mouse over the elements in an enum
        // it will show you a number. That number determines which slot the equipment should be added.
        int slotIndex = (int)newItem.equipSlot;

        if (currentEquipment[slotIndex] == null) // if the element in currentEquipment at slotIndex is empty, add newItem to it
        {
            currentEquipment[slotIndex] = newItem;
        }
        else // something is already being worn so don't add (must remove item first, clearing the slot then you can add newItem)
        {
            return false;
        } 

        if (onEquippedChangedCallback != null) // safety net so no error occur with delegate
        {
            onEquippedChangedCallback.Invoke(); // triggering the OnEquippedChanged event (you can find this in the EquipmentUI script)
        }

        return true;
    }
    // ----------------------------------------------------------------------------- //

    // ----------------- Removing an equipment item from currentEquipment ----------------- //
    public void Unequip(Equipment equipment)
    {
        int slotIndex = (int)equipment.equipSlot; // get the index of which equipment slot where this item belongs

        currentEquipment[slotIndex] = null; // emptying that element in the currentEquipment array

        if (onEquippedChangedCallback != null) // safety net so no error occur with delegate
        {
            onEquippedChangedCallback.Invoke(); // triggering the OnEquippedChanged event (you can find this in the EquipmentUI script)
        }
    }
    // ------------------------------------------------------------------------------------ //
}

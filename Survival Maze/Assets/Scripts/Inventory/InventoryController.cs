using System.Collections.Generic;
using UnityEngine;

// This script is on the player game object
// Purpose: The purpose of this script is to implement the rules and core functionality of the inventory. This includes limiting the space in the inventory,
//          keeping track of items currently in the inventory, adding/removing items from the inventory, and creating the core for updating the inventory UI.
public class InventoryController : MonoBehaviour {

    // This is a singleton, singletons are classes that are always "alive". You can call them without creating an instance of them (Class class = new Class();),
    // the way you call singletons is like this, ClassName.instance, instance is made in the Awake() function (hidden in the region, press the + on the left to show it)
    /*
    #region Singleton

    public static InventoryController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }

        instance = this;
    }

    #endregion
    */

    // -------------------------- Public variables -------------------------- //
    public int space = 20; // limiting the inventory space
    public List<Item> items = new List<Item>(); // list that holds all inventory items

    // This is a delegate, delegates are events that can have methods subscribed to them and when the event is triggered it will call all subscribed methods
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public delegate void OnResourceChanged(Item item);
    public OnResourceChanged onResourceChangedCallback;
    // ---------------------------------------------------------------------- //

    // ----------- Testing variables ----------- //
    // Get rid of these lines when Player Pick Up functionality is implemented
    public Item item;
    public Consumable[] testConsumable;
    public Equipment[] testEquipment;
    public Resource testResourceLog;
    public Resource testResourceStick;
    public GameObject testGameObject;
    // ----------------------------------------- //

    // Update() can be removed once Player Pick Up functionality is implemented
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Testing purpose only, adds testItem to inventory
        {
            Add(item);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < testEquipment.Length; i++)
            {
                Add(testEquipment[i]);
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            for (int i = 0; i < testConsumable.Length; i++)
            {
                Add(testConsumable[i]);
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Add(testResourceLog);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Add(testResourceStick);
        }
    }

    // ------------ Adding to inventory list ------------ //
    // Returns true if item was picked up, otherwise false
    public bool Add(Item item)
    {
        if (!item.isDefaultItem) // if the item isn't a default item (default items include starting weapon, etc.)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room");
                return false;
            }

            items.Add(item);

            if (onItemChangedCallback != null) // safety net so no errors occur with delegate
            {
                onItemChangedCallback.Invoke(); // triggering the OnItemChanged event (can be found in InventoryUI)
            }

            if (item.itemType == ItemType.Resource) // if item is a resource, trigger the onResourceChanged event as well (can be found in InventoryUI)
            {
                onResourceChangedCallback.Invoke(item);
            }
        }   

        return true;
    }
    // -------------------------------------------------- //

    // ---------- Removing from inventory list ---------- //
    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null) // safety net so no error occur with delegate
        {
            onItemChangedCallback.Invoke(); // triggering the OnItemChanged event
        }
    }
    // -------------------------------------------------- //

}

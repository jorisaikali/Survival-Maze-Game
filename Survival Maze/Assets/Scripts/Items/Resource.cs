using UnityEngine;

// This is a blueprint for the Resource item. It inherits from the Item class. CreateAssetMenu creates a new
// object that can be created when right clicking on the Project window. Right click on 
// Project window -> Create -> Inventory -> Resource
[CreateAssetMenu(fileName = "New Resource", menuName = "Inventory/Resource")]
public class Resource : Item {

    // ------- Public variables ------- //
    public int stackAmount = 0;
    // -------------------------------- //

}

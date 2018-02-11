using System.Collections.Generic;

// This class is a Helper class. The purpose of this class is to help the Distinct() function in InventoryUI
// determine what items in the inventory are "duplicates" and what aren't.

/* 
    Duplicate rules:
        - Any item is a duplicate if it is a Resource and the names are the same.
        - Any item that is not a Resource, even there are two or more instances of the item in the
          inventory is not considered a duplicate.
            - For example, the player is able to carry two "Sword of Poopoo"s but cannot carry two
              different sets of Logs. Logs would only take one inventory slot no matter how many you
              have stacked.
*/
public class DistinctItemComparer : IEqualityComparer<Item> {

    public bool Equals(Item x, Item y)
    {
        return (x.itemType == ItemType.Resource && y.itemType == ItemType.Resource) && (x.name == y.name);
    }

    public int GetHashCode(Item obj)
    {
        return obj.itemType.GetHashCode() ^ obj.name.GetHashCode();
    }

    
}

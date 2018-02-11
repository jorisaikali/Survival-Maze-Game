using UnityEngine;

// This is a blueprint for the Consumable item. It inherits from the Item class. CreateAssetMenu creates a new
// object that can be created when right clicking on the Project window. Right click on 
// Project window -> Create -> Inventory -> Consumable
[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item {

    // ------- Public variables ------- //
    public ConsumableType type; // Four different types as seen at the bottom of the script
    public float regenAmount;
    // -------------------------------- //

    // ------------------- Overriding the Item Use() function ------------------- //
    public override void Use(GameObject player) // overriding the Item class Use() function
    {
        bool wasConsumed = false;

        if (type == ConsumableType.healthRegen) // if consumable is meant to heal
        {
            wasConsumed = player.GetComponent<PlayerVitals>().RegenHealth(regenAmount); // heal the player and set wasConsumed to true, if player is already full health, wasConsumed = false
        }
        else if (type == ConsumableType.staminaRegen) // if consumable is meant to regen stamina
        {
            wasConsumed = player.GetComponent<PlayerVitals>().RegenStamina(regenAmount); // regen the players stamina and set wasConsumed to true, if player is already full stamina, wasConsumed = false
        }
        else if (type == ConsumableType.hungerRegen) // if consumable is meant to regen hunger
        {
            wasConsumed = player.GetComponent<PlayerVitals>().RegenHunger(regenAmount); // regen the players hunger and set wasConsumed to true, if player is already full hunger, wasConsumed = false
        }
        else if (type == ConsumableType.thirstRegen) // if consumable is meant to regen thirst
        {
            wasConsumed = player.GetComponent<PlayerVitals>().RegenThirst(regenAmount); // regen the players thrist and set wasConsumed to true, if player is already full thirst, wasConsumed = false
        }

        if (wasConsumed) // if the consumable was consumed
        {
            player.GetComponent<InventoryController>().Remove(this); // remove it from the inventory
        }
    }
    // -------------------------------------------------------------------------- //
}

public enum ConsumableType { healthRegen, staminaRegen, hungerRegen, thirstRegen }

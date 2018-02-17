using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerVitals : NetworkBehaviour {

    // -------- Public variables -------- //
    
    public float testHealthMax = 100f;

    [SyncVar]
    public float testHealth = 100f;
    public List<Vital> vitals;
    // ---------------------------------- //

    // -------- Private variables -------- //
    private PlayerMovement playerMovement;
    private enum vN { HEALTH, HUNGER, THIRST, STAMINA }
    // ----------------------------------- //

    private void Start()
    {
        // ------- Getting references to other scripts -------- //
        playerMovement = GetComponent<PlayerMovement>();
        // ---------------------------------------------------- //
    }

    public void UpdateMe()
    {
        // --------- Controlling the vitals --------- //
        HealthController(vitals); // Updates the health vital
        VitalController(vitals[(int)vN.HUNGER]); // Updates the hunger vital
        VitalController(vitals[(int)vN.THIRST]); // Updates the thirst vital
        StaminaController(vitals[(int)vN.STAMINA]); // Updates the stamina vital
        // ------------------------------------------ //
    }

    // ----------------------------------- Controlling health functionality ----------------------------------- //
    public void UpdateHealth(float value) {
        // increments or decrements the health slider value by the parameter 'value'
        
        float new_value = 0;
        //TODO: need to clamp input value to avoid unknown reactions
        if (value < 0){
            //doing damage, only from (-health_left,0)
            //new_value = Mathf.Clamp(value,-1f*vitals[(int)vN.HEALTH].slider.value, 0f);

            new_value = value;
            
        } else {
            //healing, only from (0,health_lost)
             
            new_value = value;
        }

        testHealth += value;

        Debug.Log("health: " + testHealth);

        if(testHealth <= 0) {
            Debug.Log("Player: " +transform.name + " is dead.");
        }
        
    }
    private void HealthController(List<Vital> vitals)
    {
        if (vitals[(int)vN.HUNGER].slider.value <= 0 && vitals[(int)vN.THIRST].slider.value <= 0) // if hunger is depleted AND thirst is depleted, health depletes at rate x 2
        {
            vitals[(int)vN.HEALTH].slider.value -= Time.deltaTime * vitals[(int)vN.HEALTH].fallRate * 2;
        }
        else if (vitals[(int)vN.HUNGER].slider.value <= 0 || vitals[(int)vN.THIRST].slider.value <= 0) // if hunger is depleted OR thirst is depleted, health depletes at normal rate
        {
            vitals[(int)vN.HEALTH].slider.value -= Time.deltaTime * vitals[(int)vN.HEALTH].fallRate;
        }

        if (vitals[(int)vN.HEALTH].slider.value <= 0) // if health is fully depleted, player dies
        {
            //Debug.Log("Player died");
            // TODO: Players dies
        }
    }

    // -------------------------------------------------------------------------------------------------------- //

    // ------------------- Controlling hunger/thirst functionality ------------------- //
    private void VitalController(Vital vital)
    {
        if (vital.slider.value >= 0) // if hunger/thirst is not depleted, deplete at a rate (the lower the rate the slower)
        {
            vital.slider.value -= Time.deltaTime * vital.fallRate;
        }
        else if (vital.slider.value <= 0) // if hunger/thirst is depleted, keep it at 0 (so it doesn't continue into negative values)
        {
            vital.slider.value = 0;
        }
        else if (vital.slider.value >= vital.max) // if hunger/thirst is at the max, keep it at max (until depleted, this is so if you eat food or drink you won't go over the max)
        {
            vital.slider.value = vital.max;
        }
    }
    // ------------------------------------------------------------------------------- //

    // ------------------------- Controls stamina functionality ------------------------- //
    private void StaminaController(Vital vital)
    {
        bool running = false;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W)) // if left shift is pressed AND player is moving forward
        {
            // set running to true, increase players speed, and begin depleting stamina
            running = true;
            playerMovement.moveSpeed = 10f;
            vital.slider.value -= Time.deltaTime * vital.fallRate;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.W)) // if left shift is not pressed anymore
        {
            // set running to false, and players speed back to normal
            running = false;
            playerMovement.moveSpeed = 5f;
        }

        if (!running) // if not running, regenerate stamina
        {
            vital.slider.value += Time.deltaTime * vital.fallRate;
        }

        if (vital.slider.value <= 0) // if stamina is fully depleted, keep at 0 (until left shift is let go), and players speed back to normal
        {
            vital.slider.value = 0;
            playerMovement.moveSpeed = 5f;
        }
        else if (vital.slider.value >= vital.max) // if stamina is at the max, keep it there (until shift is pressed again, this is if we have a consumable that gives stamina)
        {
            vital.slider.value = vital.max;
        }
    }
    // ---------------------------------------------------------------------------------- //

    // ------------ Regen health functionality ------------ //
    public bool RegenHealth(float amount)
    {
        // if players health is greater than threshold (0.995), don't allow the player to use regen health item (it's just a waste)
        if (vitals[(int)vN.HEALTH].slider.value >= 0.995f)
        {
            return false; // return false cause the player didn't consume the consumable
        }

        vitals[(int)vN.HEALTH].slider.value += amount; // if below threshold, increase health by amount

        return true; // return true cause the player consumed the consumable
    }
    // ---------------------------------------------------- //

    // ------------ Regen hunger functionality ------------ //
    public bool RegenHunger(float amount)
    {
        // if players hunger is greater than threshold (0.995), don't allow the player to use regen hunger item (it's just a waste)
        // the reason why this isn't == 1f is because the players hunger will never techincally be at 1f because it's constantly decreasing, this is the same for thirst
        if (vitals[(int)vN.HUNGER].slider.value >= 0.995f)
        {
            return false; // return false cause the player didn't consume the consumable
        }

        vitals[(int)vN.HUNGER].slider.value += amount; // if below threshold, increase hunger by amount

        return true; // return true cause the player consumed the consumable
    }
    // ---------------------------------------------------- //

    // ------------ Regen thirst functionality ------------ //
    public bool RegenThirst(float amount)
    {
        // if players thirst is greater than threshold (0.995), don't allow the player to use regen thirst item (it's just a waste)
        if (vitals[(int)vN.THIRST].slider.value >= 0.995f)
        {
            return false; // return false cause the player didn't consume the consumable 
        }

        vitals[(int)vN.THIRST].slider.value += amount; // if below threshold, increase thirst by amount

        return true; // return true cause the player consumed the consumable
    }
    // ---------------------------------------------------- //

    // ------------ Regen stamina functionality ------------ //
    public bool RegenStamina(float amount)
    {
        // if players stamina is greater than threshold (0.995), don't allow the player to use regen stamina item (it's just a waste)
        if (vitals[(int)vN.STAMINA].slider.value >= 0.995f)
        {
            return false; // return false cause the player didn't consume the consumable
        }

        vitals[(int)vN.STAMINA].slider.value += amount; // if below threshold, increase stamina by amount

        return true; // return true cause the player consumed the consumable
    }
    // ----------------------------------------------------- //
}

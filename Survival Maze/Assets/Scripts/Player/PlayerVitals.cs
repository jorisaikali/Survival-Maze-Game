using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVitals : MonoBehaviour {

    // -------- Public variables -------- //
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

    private void Update()
    {
        // --------- Controlling the vitals --------- //
        HealthController(vitals); // Updates the health vital
        VitalController(vitals[(int)vN.HUNGER]); // Updates the hunger vital
        VitalController(vitals[(int)vN.THIRST]); // Updates the thirst vital
        StaminaController(vitals[(int)vN.STAMINA]); // Updates the stamina vital
        // ------------------------------------------ //
    }

    // ----------------------------------- Controlling health functionality ----------------------------------- //
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
            Debug.Log("Player died");
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
}

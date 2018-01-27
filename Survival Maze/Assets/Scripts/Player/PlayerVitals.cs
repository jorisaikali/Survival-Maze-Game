using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVitals : MonoBehaviour {

    public List<Vital> vitals;

    private PlayerMovement playerMovement;
    private enum vN { HEALTH, HUNGER, THIRST, STAMINA }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        HealthController(vitals);
        VitalController(vitals[(int)vN.HUNGER]);
        VitalController(vitals[(int)vN.THIRST]);
        StaminaController(vitals[(int)vN.STAMINA]);
    }

    private void HealthController(List<Vital> vitals)
    {
        if (vitals[(int)vN.HUNGER].slider.value <= 0 && vitals[(int)vN.THIRST].slider.value <= 0)
        {
            vitals[(int)vN.HEALTH].slider.value -= Time.deltaTime * vitals[(int)vN.HEALTH].fallRate * 2;
        }
        else if (vitals[(int)vN.HUNGER].slider.value <= 0 || vitals[(int)vN.THIRST].slider.value <= 0)
        {
            vitals[(int)vN.HEALTH].slider.value -= Time.deltaTime * vitals[(int)vN.HEALTH].fallRate;
        }

        if (vitals[(int)vN.HEALTH].slider.value <= 0)
        {
            Debug.Log("Player died");
            // TODO: Players dies
        }
    }

    private void VitalController(Vital vital)
    {
        if (vital.slider.value >= 0)
        {
            vital.slider.value -= Time.deltaTime * vital.fallRate;
        }
        else if (vital.slider.value <= 0)
        {
            vital.slider.value = 0;
        }
        else if (vital.slider.value >= vital.max)
        {
            vital.slider.value = vital.max;
        }
    }

    private void StaminaController(Vital vital)
    {
        bool running = false;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0f)
        {
            running = true;
            playerMovement.moveSpeed = 10f;
            vital.slider.value -= Time.deltaTime * vital.fallRate;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;
            playerMovement.moveSpeed = 5f;
        }

        if (!running)
        {
            vital.slider.value += Time.deltaTime * vital.fallRate;
        }

        if (vital.slider.value <= 0)
        {
            vital.slider.value = 0;
            playerMovement.moveSpeed = 5f;
        }
        else if (vital.slider.value >= vital.max)
        {
            vital.slider.value = vital.max;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	// Aggregate all components for updating
	PlayerCombat playerCombat;
	PlayerMovement playerMovement;
	PlayerVitals playerVitals;
    private string ID;
	void Start () {
		// Tag the local player to avoid self-collisions
		if(isLocalPlayer) {
			gameObject.tag = "LocalPlayer";
		}

		playerCombat = GetComponent<PlayerCombat>();
		playerMovement = GetComponent<PlayerMovement>();
		playerVitals = GetComponent<PlayerVitals>();

        // get a unique name for player
        ID = "P"+GetComponent<NetworkIdentity>().netId;
        transform.name = ID;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer) {
			return;
		}
		playerCombat.UpdateMe();
		playerMovement.UpdateMe();
		playerVitals.UpdateMe();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	// Aggregate all components for updating
	PlayerCombat playerCombat;
	PlayerMovement playerMovement;
	PlayerVitals playerVitals;

	void Start () {
		playerCombat = GetComponent<PlayerCombat>();
		playerMovement = GetComponent<PlayerMovement>();
		playerVitals = GetComponent<PlayerVitals>();

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

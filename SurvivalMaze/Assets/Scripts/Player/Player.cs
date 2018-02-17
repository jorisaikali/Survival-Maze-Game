using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerVitals))]
public class Player : NetworkBehaviour {

	// Aggregate all components for updating
	PlayerCombat playerCombat;
	PlayerMovement playerMovement;
	PlayerVitals playerVitals;
	void Start () {
		// Tag the local player to avoid self-collisions
		if(isLocalPlayer) {
			gameObject.tag = "LocalPlayer";
		}

		playerCombat = GetComponent<PlayerCombat>();
		playerMovement = GetComponent<PlayerMovement>();
		playerVitals = GetComponent<PlayerVitals>();

        // get a unique name for player
	}


	public void TakeDamage(float dmg) {
		playerVitals = GetComponent<PlayerVitals>();
		Debug.Log("Damage called : "+ playerVitals);
		playerVitals.UpdateHealth(dmg);
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

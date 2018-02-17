using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Weapon : MonoBehaviour {


	private PlayerCombat player_combat;

	void Start() {
		player_combat = GetComponentInParent<PlayerCombat>();
	}


	void OnTriggerEnter(Collider other){

		if(other.gameObject.layer == 8 && other.gameObject.tag != "LocalPlayer") { //if collide with anything in entities layer
			//all entities must have vitals

			//other.SendMessage("CmdApplyDamage",-1f); // pretty weird but make the player send damage to itself
			Debug.Log("Triggered: " + player_combat.transform.name);
			//player_combat.Callme();
			//player_combat.CmdApplyDamage(-3);
			other.SendMessage("CmdApplyDamage",-3); // apparently this is expensive?
			//other.GetComponent<PlayerCombat>().CmdApplyDamage(-3);
		}
	}
    // Server commands can only use primitives as parameters therefore we need to use ids rather than game object

    // ------------Weapon attack animation and return animation -------- //

    public void ReturnStance(){
		transform.Rotate(-50f,0f,0f);
	}

	public void Attack(){
		transform.Rotate(50f,0f,0f);
	}
	// ---------------------------------------------------------------- //
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Weapon : NetworkBehaviour {

	void OnTriggerEnter(Collider other){

		if(other.gameObject.layer == 8 && other.gameObject.tag != "LocalPlayer") { //if collide with anything in entities layer
			//all entities must have vitals
			 //this calls the entity's method UpdateHealth
			//Debug.Log("weapon collision");
		}
	}
	void ApplyDamage(GameObject target) {
		Debug.Log("hit other player");
		//target.SendMessage("UpdateHealth",-1f);
	}

	// ------------Weapon attack animation and return animation -------- //
	public void ReturnStance(){
		transform.Rotate(-50f,0f,0f);
	}
	public void Attack(){
		transform.Rotate(50f,0f,0f);
	}
	// ---------------------------------------------------------------- //
}

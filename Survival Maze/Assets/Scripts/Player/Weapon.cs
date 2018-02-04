using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	CapsuleCollider weapon;

	void Start() {
		weapon = GetComponent<CapsuleCollider>();
	}

	void OnTriggerEnter(Collider other){

		if(other.gameObject.layer == 8) { //if collide with anything in entities layer
			//all entities must have vitals
			other.gameObject.SendMessage("UpdateHealth",-1f); //this calls the entity's method UpdateHealth
			Debug.Log("weapon collision");
		}
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

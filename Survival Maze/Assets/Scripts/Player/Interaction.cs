using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable: MonoBehaviour {
	// This class needs more development specifically drawing out the hierarchy of object relations
	public float radius = 3f;
	void OnDrawGizmosSelected () { 
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position,radius);
	}

	void Interact(){
		// interact witht the game object
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	// ------------Weapon attack animation and return animation -------- //
	public void ReturnStance(){
		transform.Rotate(-50f,0f,0f);
	}
	public void Attack(){
		transform.Rotate(50f,0f,0f);
	}
	// ---------------------------------------------------------------- //
}

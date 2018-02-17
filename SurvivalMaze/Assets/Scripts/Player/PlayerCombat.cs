using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


//--------- Required Components-------//
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(PlayerStats))]
//------------------------------------//
public class PlayerCombat : NetworkBehaviour {

	
	PlayerMovement playerMovement;
	Weapon weapon;
	PlayerStats playerStats;

	PlayerVitals playerVitals;


	void Start () {
		// Initialize
		playerVitals = GetComponent<PlayerVitals>();
		playerMovement = GetComponent<PlayerMovement>();
		weapon = GetComponentInChildren<Weapon>();
		playerStats = GetComponent<PlayerStats>();

	}
	

	public void UpdateMe () {

		//------------Mouse left click attack action ------------//
		if(Input.GetMouseButtonDown(0)){
			//DoDamage();
			weapon.Attack();// Trigger animation


		} else if (Input.GetMouseButtonUp(0)) {
			weapon.ReturnStance();
		}
		//--------------------------------------------------------//
	}

	public void Callme() {
		Debug.Log("called on "+ transform.name);
	}

	[Client]
	void DoDamage(){
		GameObject target = playerMovement.GetRayCast(LayerMask.GetMask("Entities")); // layer 8 is all entities
        if (target != null){
			//PlayerVitals target_vitals = target.GetComponent<PlayerVitals>(); // expensive operation
            //Debug.Log(target_vitals);
			//target_vitals.UpdateHealth(playerStats.damage);
        }
	}

	[Command]
	public void CmdApplyDamage(int dmg) {
		PlayerVitals playerVitals = GameManager.GetPlayer(transform.name);
		playerVitals.UpdateHealth(-3);
	}
	
}

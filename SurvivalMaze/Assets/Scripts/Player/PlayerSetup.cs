using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerVitals))]
public class PlayerSetup : NetworkBehaviour {

	// Use this for initialization
	[SerializeField]
	Behaviour[] componentsToDisable;
	void Start () {
		if(!isLocalPlayer) {
			for (int i = 0 ; i < componentsToDisable.Length; i++){
				componentsToDisable[i].enabled = false;
			}
		}
	}

	public override void OnStartClient() {
		base.OnStartClient();

		string netID = GetComponent<NetworkIdentity>().netId.ToString();
		PlayerVitals playerVitals = GetComponent<PlayerVitals>();
		GameManager.RegisterPlayer(netID, playerVitals);

	}

	void OnDisable () {
		GameManager.DeregisterPlayer(transform.name);
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
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
	
	// Update is called once per frame
	void Update () {
		
	}
}

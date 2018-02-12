﻿using UnityEngine;
using UnityEngine.Networking;
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    private GameObject canvas;

	// Use this for initialization
	[SerializeField]
	Behaviour[] componentsToDisable;
	void Start () {
		if(!isLocalPlayer) {
			for (int i = 0 ; i < componentsToDisable.Length; i++){
				componentsToDisable[i].enabled = false;
			}

            canvas.SetActive(false);
		}
	}
}

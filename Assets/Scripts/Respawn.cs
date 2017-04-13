using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

	public playerController Player;

	void Start () {
		
	}
	

	void OnTriggerEnter (Collider col) {

		if (col.gameObject.CompareTag ("Player")) {
			//Debug.Log ("You have been caught!");
			Player.Respawn ();
		}

		/*if (col.gameObject.CompareTag ("Bolt")) {
			Debug.Log ("You shot the AI!");
			//AI.SetState ("STUNNED");
		}*/
	}
}

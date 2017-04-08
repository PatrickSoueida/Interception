using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camouflage : MonoBehaviour {

	public string color;
	public playerController player;

	void Start () {
		
	}

	void Update () {
		
	}

	void OnTriggerStay(Collider coll){
		if(coll.CompareTag("Player")){
			if (player.GetColor () == color) {
				player.SetCamo (true);
			} 
			else {
				player.SetCamo (false);
			}
		}
	}

	void OnTriggerExit(Collider coll){
		if(coll.CompareTag("Player")){
			player.SetCamo (false);
		}
	}
}

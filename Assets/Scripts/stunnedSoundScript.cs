using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stunnedSoundScript : MonoBehaviour {

    public AudioSource stunSound;
    AudioSource myStunSound;

	void Start () 
    {
        myStunSound = stunSound.GetComponent<AudioSource>();

        Instantiate(myStunSound);
	}
	
}

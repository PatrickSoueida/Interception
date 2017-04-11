using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class searchSoundScript : MonoBehaviour 
{
    public AudioSource searchSound;
    AudioSource mySearchSound;
	
	void Start () 
    {
        mySearchSound = searchSound.GetComponent<AudioSource>();

        Instantiate(mySearchSound);
	}

}

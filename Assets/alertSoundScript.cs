using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alertSoundScript : MonoBehaviour 
{
    public AudioSource alertSound;
    AudioSource myAlertSound;

    void Start () 
    {
        myAlertSound = alertSound.GetComponent<AudioSource>();

        Instantiate(myAlertSound);
	}
}

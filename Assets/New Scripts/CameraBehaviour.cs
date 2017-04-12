using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour 
{
     public Transform targetPosition;
     public Transform targetRotation; //Optional of course

     public Transform initialPosition;
     public Transform initialRotation;

     public float smoothFactor = 2;
     bool buttonPressed;
     bool soundPlayed;
     bool soundPlayed2;
     public bool obstructed;
     float obstTime = 0;

    public AudioSource aimingSound;
    AudioSource myAimingSound;
    float timeToMove = 0.1f;

    public GameObject crossHair;

	// Use this for initialization
	void Start () 
    {
		GetComponent<Camera> ().enabled = true;
        crossHair.SetActive(false);
        soundPlayed2 = false;
        myAimingSound = aimingSound.GetComponent<AudioSource>();
        soundPlayed = false;
        buttonPressed = false;
        obstructed = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            if(soundPlayed == false)
            {
                soundPlayed = true;
                Instantiate(myAimingSound);
            }
            buttonPressed = true;
            soundPlayed2 = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            if(soundPlayed2 == false)
            {
                soundPlayed2 = true;
                Instantiate(myAimingSound);
            }
            buttonPressed = false;
            soundPlayed = false;
        }

        if (obstructed)
        {
            obstTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition.position, obstTime/timeToMove);
        }
        else obstTime = 0f;
        if (buttonPressed == true)
        {
            crossHair.SetActive(true);
            transform.position = Vector3.Lerp(transform.position, targetPosition.position, Time.deltaTime * smoothFactor);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation.rotation, Time.deltaTime * smoothFactor);
        }

        if (buttonPressed == false)
        {
            crossHair.SetActive(false);
            transform.position = Vector3.Lerp(transform.position, initialPosition.position, Time.deltaTime * smoothFactor);
            //transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation.rotation, Time.deltaTime * smoothFactor); 
        }
	}

    public bool GetButtonPressed()
    {
        return buttonPressed;
    }
}

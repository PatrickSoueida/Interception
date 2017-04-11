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

    public AudioSource aimingSound;
    AudioSource myAimingSound;

    public GameObject crossHair;

	float yaw, pitch;
	float mouseSensitivity = 4;
	public Transform target;
	float distanceFromTarget = 15;
	Vector2 pitchMinMax = new Vector2 (-40, 85);
	float rotationSmoothTime = .15f;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;


	// Use this for initialization
	void Start () 
    {
        crossHair.SetActive(false);
        soundPlayed2 = false;
        myAimingSound = aimingSound.GetComponent<AudioSource>();
        soundPlayed = false;
        buttonPressed = false;
	}

	void LateUpdate()
	{
		yaw += Input.GetAxis ("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

		transform.eulerAngles = currentRotation;

		transform.position = target.position - transform.forward * distanceFromTarget;

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
}

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
     public bool buttonPressed;

	// Use this for initialization
	void Start () 
    {
        
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || (Input.GetKeyDown(KeyCode.JoystickButton4)))
            buttonPressed = true;

        else if (Input.GetKeyUp(KeyCode.Alpha1) || (Input.GetKeyUp(KeyCode.JoystickButton4)))
            buttonPressed = false;

        if (buttonPressed == true) 
        { 
                 transform.position = Vector3.Lerp(transform.position, targetPosition.position, Time.deltaTime * smoothFactor);
                 transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation.rotation, Time.deltaTime * smoothFactor);
        }

        else if (buttonPressed == false)
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition.position, Time.deltaTime * smoothFactor);
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation.rotation, Time.deltaTime * smoothFactor); 
        }
		



	}
}

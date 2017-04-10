using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour 
{
    public float time = 15.0f;
    public Transform mapVantagePoint, characterVantagePoint, keyVantagePoint, gateVantagePoint, enemyVantagePoint, characterCameraPoint;
    public float smoothFactor;
    public Camera camera;
	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        time -= Time.deltaTime; 

        if (time <= 15.0f)
        {
            transform.position = mapVantagePoint.position;
            transform.rotation = mapVantagePoint.rotation;
            //transform.position = Vector3.Lerp(transform.position, targetPosition.position, Time.deltaTime * smoothFactor); 
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation.rotation, Time.deltaTime * smoothFactor);
        }

        if (time <= 11.0f)
        {
            transform.position = characterVantagePoint.position;
            transform.rotation = characterVantagePoint.rotation;
            //transform.position = Vector3.Lerp(transform.position, targetPosition.position, Time.deltaTime * smoothFactor);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation.rotation, Time.deltaTime * smoothFactor);
        }

        if (time <= 7.0f)
        {
            transform.position = keyVantagePoint.position;
            transform.rotation = keyVantagePoint.rotation;
            //transform.position = Vector3.Lerp(transform.position, targetPosition.position, Time.deltaTime * smoothFactor);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation.rotation, Time.deltaTime * smoothFactor);
        }

        if (time <= 3.0f)
        {
            transform.position = gateVantagePoint.position;
            transform.rotation = gateVantagePoint.rotation;
            //transform.position = Vector3.Lerp(transform.position, targetPosition.position, Time.deltaTime * smoothFactor);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation.rotation, Time.deltaTime * smoothFactor);
        }

        if (time <= -1.0f)
        {
            transform.position = enemyVantagePoint.position;
            transform.rotation = enemyVantagePoint.rotation;
            //transform.position = Vector3.Lerp(transform.position, targetPosition.position, Time.deltaTime * smoothFactor);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation.rotation, Time.deltaTime * smoothFactor);
        }

        if (time <= -4.0f)
        {
            Destroy(this.gameObject);
        }

	}
}

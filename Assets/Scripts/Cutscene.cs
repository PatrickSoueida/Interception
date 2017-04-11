using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour 
{
    public float time = 15.0f;
    public Transform mapVantagePoint, characterVantagePoint, keyVantagePoint, gateVantagePoint, enemyVantagePoint, characterCameraPoint;
    public float smoothFactor;
    public Camera camera;
    float timeOne = 0.0f;
    float timeTwo = 0.0f;
    float timeThree = 0.0f;
    float timeFour = 0.0f;
    float timeFive = 0.0f;
    public bool destroy;

    float timeToMove = 2.0f;
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
            timeOne += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, mapVantagePoint.position, timeOne / timeToMove); 
            transform.rotation = Quaternion.Slerp(transform.rotation, mapVantagePoint.rotation,  timeOne / timeToMove);
        }

        if (time <= 11.0f)
        {
            timeTwo += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, characterVantagePoint.position, timeTwo / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, characterVantagePoint.rotation, timeTwo / timeToMove);
        }

        if (time <= 7.0f)
        {
            timeThree += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, keyVantagePoint.position,  timeThree / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, keyVantagePoint.rotation,  timeThree / timeToMove);
        }

        if (time <= 3.0f)
        {
            //timeFour += Time.deltaTime;
            //transform.position = Vector3.Lerp(transform.position, gateVantagePoint.position, timeFour / timeToMove);
            //transform.rotation = Quaternion.Slerp(transform.rotation, gateVantagePoint.rotation, timeFour / timeToMove);
        }

        if (time <= -1.0f)
        {
            timeFive += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, enemyVantagePoint.position, timeFive / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, enemyVantagePoint.rotation, timeFive / timeToMove);
        }

        if (time <= -4.0f)
        {
            destroy = true;
            Destroy(this.gameObject);
        }

	}
}

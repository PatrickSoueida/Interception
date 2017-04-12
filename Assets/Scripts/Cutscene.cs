using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour 
{

    public GameObject Player;
    public float time = 25.0f;
    public Transform mapVantagePoint, characterVantagePoint, keyVantagePoint, gateVantagePoint, enemyVantagePoint, camoVantagePointOne, camoVantagePointTwo, camoVantagePointThree;
    public float smoothFactor;
    public Camera camera;
    float timeOne = 0.0f;
    float timeTwo = 0.0f;
    float timeThree = 0.0f;
    float timeFour = 0.0f;
    float timeFive = 0.0f;
    float timeSix = 0.0f;
    float timeSeven = 0.0f;
    float timeEight = 0.0f;

    public bool destroy;

    float timeToMove = 2.0f;
	// Use this for initialization
	void Start () 
    {
        Player.GetComponent<playerController>().enabled = false;
	}

    void OnGUI()
    {
       // GUI.Label(new Rect(Screen.width / 2 - 900 / 2, Screen.height / 2, 900,  20), "Welcome to reb00t!");
    }
	
	// Update is called once per frame
	void Update () 
    {
        time -= Time.deltaTime;
        if (time <= 25.0f)
        {
            timeOne += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, mapVantagePoint.position, timeOne / timeToMove); 
            transform.rotation = Quaternion.Slerp(transform.rotation, mapVantagePoint.rotation,  timeOne / timeToMove);
        }

        if (time <= 21.0f)
        {
            timeTwo += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, characterVantagePoint.position, timeTwo / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, characterVantagePoint.rotation, timeTwo / timeToMove);
        }

        if (time <= 17.0f)
        {
            timeThree += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, keyVantagePoint.position,  timeThree / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, keyVantagePoint.rotation,  timeThree / timeToMove);
        }

        if (time <= 13.0f)
        {
            timeFour += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, gateVantagePoint.position, timeFour / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, gateVantagePoint.rotation, timeFour / timeToMove);
        }

        if (time <= 9.0f)
        {
            timeFive += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, enemyVantagePoint.position, timeFive / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, enemyVantagePoint.rotation, timeFive / timeToMove);
        }

        if (time <= 6.0f)
        {
            timeSix += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, camoVantagePointOne.position, timeSix / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, camoVantagePointOne.rotation, timeSix / timeToMove);
        }

        if (time <= 3.0f)
        {
            timeSeven += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, camoVantagePointTwo.position, timeSeven / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, camoVantagePointTwo.rotation, timeSeven / timeToMove);
        }

        if (time <= 0.0f)
        {
            timeEight += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, camoVantagePointThree.position, timeEight / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, camoVantagePointThree.rotation, timeEight / timeToMove);
        }

        if (time <= -4.0f || Input.GetKeyDown(KeyCode.Space))
        {
            Player.GetComponent<playerController>().enabled = true;
            Destroy(this.gameObject);
        }

	}
}

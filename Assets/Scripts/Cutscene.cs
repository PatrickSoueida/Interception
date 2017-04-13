using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour 
{

    public GameObject Player;
    public List<GameObject> finalSwitchesList;
    public float time = 25.0f;
    public Transform mapVantagePoint, characterVantagePoint, gateVantagePoint, enemyVantagePoint, camoVantagePointOne, camoVantagePointTwo, camoVantagePointThree;
    Transform keyVantagePointOne, keyVantagePointTwo, keyVantagePointThree, keyVantagePointFour;
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
    float timeNine = 0.0f;
    float timeTen = 0.0f;
    float timeEleven = 0.0f;


    Vector3 temp = new Vector3(0.0f, 7.0f, -7.0f);

    public bool destroy;

    float timeToMove = 1.0f;
	// Use this for initialization
	void Start () 
    {
        finalSwitchesList = GameObject.FindGameObjectWithTag("SwitchController").GetComponent<switchScript>().finalPickedSwitches;
        Player.GetComponent<playerController>().enabled = false;
        keyVantagePointOne.position = finalSwitchesList[0].transform.GetChild(17).gameObject.transform.position;
        keyVantagePointOne.rotation = finalSwitchesList[0].transform.GetChild(17).gameObject.transform.rotation;
        keyVantagePointTwo.position = finalSwitchesList[1].transform.GetChild(17).gameObject.transform.position;
        keyVantagePointTwo.rotation = finalSwitchesList[1].transform.GetChild(17).gameObject.transform.rotation;
        keyVantagePointThree.position = finalSwitchesList[2].transform.GetChild(17).gameObject.transform.position;
        keyVantagePointThree.rotation = finalSwitchesList[2].transform.GetChild(17).gameObject.transform.rotation;
        keyVantagePointFour.position = finalSwitchesList[3].transform.GetChild(17).gameObject.transform.position;
        keyVantagePointFour.rotation = finalSwitchesList[3].transform.GetChild(17).gameObject.transform.rotation;

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
            transform.position = Vector3.Lerp(transform.position, keyVantagePointOne.position, timeThree / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, keyVantagePointOne.rotation, timeThree / timeToMove);
        }

        if (time <= 15.5f)
        {
            timeFour += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, keyVantagePointTwo.position, timeFour / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, keyVantagePointTwo.rotation, timeFour / timeToMove);
        }

        if (time <= 14.0f)
        {
            timeFive += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, keyVantagePointThree.position, timeFive / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, keyVantagePointThree.rotation, timeFive / timeToMove);
        }

        if (time <= 17.0f)
        {
            timeSix += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, keyVantagePointFour.position, timeSix / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, keyVantagePointFour.rotation, timeSix / timeToMove);
        }

        if (time <= 13.0f)
        {
            timeSeven += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, gateVantagePoint.position, timeSeven / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, gateVantagePoint.rotation, timeSeven / timeToMove);
        }

        if (time <= 9.0f)
        {
            timeEight += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, enemyVantagePoint.position, timeEight / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, enemyVantagePoint.rotation, timeEight / timeToMove);
        }

        if (time <= 6.0f)
        {
            timeNine += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, camoVantagePointOne.position, timeNine / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, camoVantagePointOne.rotation, timeNine / timeToMove);
        }

        if (time <= 3.0f)
        {
            timeTen += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, camoVantagePointTwo.position, timeTen / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, camoVantagePointTwo.rotation, timeTen / timeToMove);
        }

        if (time <= 0.0f)
        {
            timeEleven += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, camoVantagePointThree.position, timeEleven / timeToMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, camoVantagePointThree.rotation, timeEleven / timeToMove);
        }

        if (time <= -2.0f || Input.GetKeyDown(KeyCode.Space))
        {
            Player.GetComponent<playerController>().enabled = true;
            Destroy(this.gameObject);
        }

	}
}

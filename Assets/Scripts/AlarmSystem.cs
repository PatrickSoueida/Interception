using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSystem : MonoBehaviour 
{
    public bool alarm; 
    public GameObject[] allEnemies; 
	// Use this for initialization
	void Start () 
    {
        allEnemies = GameObject.FindGameObjectsWithTag("AI");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (alarm == true)
        {
            for (int i = 0; i < allEnemies.Length; i++)
            {
               // allEnemies[i].GetComponent<BasicAI>().sawPlayer == true; trying to figure out a weird bug with why it doesn't see basicAI
            }
        }
		
	}
}

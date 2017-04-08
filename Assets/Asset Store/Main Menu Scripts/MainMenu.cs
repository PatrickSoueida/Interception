using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MainMenu : MonoBehaviour 
{
    public bool isStart;
    public bool isQuit;

	// Use this for initialization
	void Start () {
		
	}

    void OnMouseUp()
    {
        if (isStart)
        {
            Application.LoadLevel(1);
        }
        if (isQuit)
        {
            Application.Quit();
        }
    } 
	// Update is called once per frame
	void Update () {
		
	}
}

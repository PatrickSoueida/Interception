using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour {
    public Color colorStart;
    public Color colorEnd = Color.green;
    public Renderer rend; 

	// Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        colorStart = rend.material.color;
    }

    void OnMouseEnter()
    {
        rend.material.color = colorEnd;
    }

    void OnMouseExit()
    {
        rend.material.color = colorStart;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

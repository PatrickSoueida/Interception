﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBolt : MonoBehaviour {

    GameObject impactRef;
    Rigidbody rbRef;
    public float speed;
    Vector3 dir;
    float lifeTime = 5f;
    float flyTime = 0f;

	// Use this for initialization
	void Awake () {
        rbRef = GetComponent<Rigidbody>();
        dir = Vector3.forward;
        
        rbRef.velocity = transform.forward * speed;

	}
	
	// Update is called once per frame
	void Update () {
        if (flyTime >= lifeTime)
            Destroy(gameObject);
        else
        {
            flyTime += Time.deltaTime;
        }
            
    }

    public void setDir(Vector3 d)
    {
        dir = d;
        rbRef.velocity = dir * speed;
    }

    void OnCollisionEnter(Collision col)
    {
        //GameObject eff = Instantiate(impactRef, transform.position, transform.rotation);
		if(!col.gameObject.CompareTag("Player") || !col.gameObject.CompareTag("Ground")){
            //Destroy(gameObject);
		}
    }
}

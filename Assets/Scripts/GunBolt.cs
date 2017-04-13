using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBolt : MonoBehaviour 
{

    GameObject impactRef;
    Rigidbody rbRef;
    public float speed;
    Vector3 dir;
    float flyTime;

	// Use this for initialization
	void Awake () 
    {
        rbRef = GetComponent<Rigidbody>();
        dir = Vector3.forward;
        
        rbRef.velocity = transform.forward * speed;

        flyTime = Time.time + 1f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Time.time > flyTime)
        {
            Destroy(gameObject);
        }      
    }

    public void setDir(Vector3 d)
    {
        dir = d;
        rbRef.velocity = dir * speed;
    }

    /*void OnCollisionEnter(Collision col)
    {
        GameObject obj = col.gameObject;
        //GameObject eff = Instantiate(impactRef, transform.position, transform.rotation);

        if(obj.tag == "Enemy")
        {
            Destroy(gameObject);
		}
    }*/
}

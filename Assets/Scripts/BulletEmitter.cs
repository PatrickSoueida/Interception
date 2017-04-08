using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmitter : MonoBehaviour {

    public GameObject bulletRef;
    GameObject gunRef;
    //GameObject crosshare;

	// Use this for initialization
	void Start () {
        gunRef = GameObject.Find("Spase_Gun");
        if (gunRef == null)
            print("no gun for you!");

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject shot = Instantiate(bulletRef, transform.position, transform.rotation);
            GunBolt bolt = shot.GetComponent<GunBolt>();
            bolt.setDir(transform.forward); //gun is sideways for some weird reason
        }
	}
}

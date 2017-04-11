 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchScript : MonoBehaviour 
{
    public GameObject portal;

    public AudioSource exitSound;
    AudioSource myExitSound;

    public AudioSource switchSound;
    AudioSource mySwitchSound;

    public AudioSource secretSound;
    AudioSource mySecretSound;

    public bool switch1;
    public bool switch2;
    public bool switch3;
    public bool switch4;

    bool portalActive;

    GameObject[] possibleSpawns;
    public GameObject spawn0;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    public GameObject spawn5;
    public GameObject spawn6;
    public GameObject spawn7;
    public GameObject spawn8;

    int randomNumber;

    int[] picked;
    bool alreadyPicked;
    int instantiated;

	void Start () 
    {
        randomNumber = 0;
        instantiated = 0;
        alreadyPicked = false;
        picked = new int[4];
        possibleSpawns = new GameObject[]{spawn0, spawn1, spawn2, spawn3, spawn4, spawn5, spawn6, spawn7, spawn8};

        while(instantiated < 4)
        {
            alreadyPicked = false;

            //new random number between 0 and 8
            randomNumber = Random.Range(0, 9);

            for(int y = 0; y < picked.Length; y++)
            {
                if(randomNumber == picked[y])
                {
                    alreadyPicked = true;
                }
            }

            if(alreadyPicked == false)
            {
                picked[instantiated] = randomNumber;
                possibleSpawns[randomNumber].SetActive(true);
                possibleSpawns[randomNumber].tag = "Switch" + (instantiated + 1);
                instantiated++;
            }
        }
            
        mySecretSound = secretSound.GetComponent<AudioSource>();
        mySwitchSound = switchSound.GetComponent<AudioSource>();
        portalActive = false;
        myExitSound = exitSound.GetComponent<AudioSource>();

        switch1 = false;
        switch2 = false;
        switch3 = false;
        switch4 = false;
    }

    void Update()
    {
        if(portalActive == false && switch1 == true && switch2 == true && switch3 == true && switch4 == true)
        {
            Instantiate(mySecretSound);
            Instantiate(myExitSound);
            portalActive = true;
            portal.SetActive(true);
        }
    }

    public void ActivateSwitch1()
    {
        if(switch1 == false)
        {
            switch1 = true;
            Instantiate(mySwitchSound);
        }
    }

    public void ActivateSwitch2()
    {
        if(switch2 == false)
        {
            switch2 = true;
            Instantiate(mySwitchSound);
        }
    }

    public void ActivateSwitch3()
    {
        if(switch3 == false)
        {
            switch3 = true;
            Instantiate(mySwitchSound);
        }
    }

    public void ActivateSwitch4()
    {
        if(switch4 == false)
        {
            switch4 = true;
            Instantiate(mySwitchSound);
        }
    }
}

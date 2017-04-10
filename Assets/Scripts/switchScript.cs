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

    public bool switch1;
    public bool switch2;
    public bool switch3;
    public bool switch4;
    public bool switch5;

    bool portalActive;

	void Start () 
    {
        mySwitchSound = switchSound.GetComponent<AudioSource>();
        portalActive = false;
        myExitSound = exitSound.GetComponent<AudioSource>();

        switch1 = false;
        switch2 = false;
        switch3 = false;
        switch4 = false;
        switch5 = false;
    }

    void Update()
    {
        if(portalActive == false && switch1 == true && switch2 == true && switch3 == true && switch4 == true && switch5 == true)
        {
            portalActive = true;
            Instantiate(myExitSound, portal.transform);
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

    public void ActivateSwitch5()
    {
        if(switch5 == false)
        {
            switch5 = true;
            Instantiate(mySwitchSound);
        }
    }
}

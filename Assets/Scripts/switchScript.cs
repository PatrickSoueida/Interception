using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchScript : MonoBehaviour 
{
    public GameObject portal;

    bool switch1;
    bool switch2;
    bool switch3;
    bool switch4;
    bool switch5;

	void Start () 
    {
        switch1 = false;
        switch2 = false;
        switch3 = false;
        switch4 = false;
        switch5 = false;
    }

    public void ActivateSwitch1()
    {
        switch1 = true;

        if(switch2 == true && switch3 == true && switch4 == true && switch5 == true)
        {
            portal.SetActive(true);
        }
    }

    public void ActivateSwitch2()
    {
        switch2 = true;

        if(switch1 == true && switch3 == true && switch4 == true && switch5 == true)
        {
            portal.SetActive(true);
        }
    }

    public void ActivateSwitch3()
    {
        switch3 = true;

        if(switch1 == true && switch2 == true && switch4 == true && switch5 == true)
        {
            portal.SetActive(true);
        }
    }

    public void ActivateSwitch4()
    {
        switch4 = true;

        if(switch1 == true && switch2 == true && switch3 == true && switch5 == true)
        {
            portal.SetActive(true);
        }
    }

    public void ActivateSwitch5()
    {
        switch5 = true;

        if(switch1 == true && switch2 == true && switch3 == true && switch4 == true)
        {
            portal.SetActive(true);
        }
    }
}

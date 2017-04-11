using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadMenu : MonoBehaviour 
{
    bool load;

    void Start()
    {
        load = false;
    }

    void Update()
    {
        if(load == false)
        {
            load = true;
           // SceneManager.LoadScene("MainMenu");
            Invoke("LoadMenu", 2f);
        }
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

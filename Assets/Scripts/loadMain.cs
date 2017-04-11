using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadMain : MonoBehaviour 
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
            Invoke("LoadMain", 2f);
        }
    }

    void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }
}

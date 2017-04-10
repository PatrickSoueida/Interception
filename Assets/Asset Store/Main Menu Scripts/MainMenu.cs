using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
    public bool isStart;
    public bool isQuit;

    public AudioSource menuSelectionSound;
    AudioSource myMenuSelectionSound;

    void Start()
    {
        myMenuSelectionSound = menuSelectionSound.GetComponent<AudioSource>();
    }

    void OnMouseUp()
    {
        if (isStart)
        {
            Instantiate(myMenuSelectionSound);
            Invoke("LoadMain", 1.5f);
        }
        if (isQuit)
        {
            Application.Quit();
        }
    } 

    void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }
}

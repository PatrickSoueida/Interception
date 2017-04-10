using UnityEngine;
using System.Collections;

public class resumeScript : MonoBehaviour 
{
    public GameObject menu;

    public AudioSource closePause;
    AudioSource myClosePause;

    void Start()
    {
        myClosePause = closePause.GetComponent<AudioSource>();
    }

    public void play()
    {
        menu.SetActive(false);
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        Instantiate(myClosePause);
    }
}

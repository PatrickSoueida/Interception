using UnityEngine;
using System.Collections;

public class BetweenLevel : MonoBehaviour {

    public GameObject loadingScreen;
    public AudioSource startSound;
    AudioSource myStartSound;

    void Start()
    {
        myStartSound = startSound.GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void play()
	{
        Instantiate(myStartSound);
        transform.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        loadingScreen.SetActive(true);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class quitScript : MonoBehaviour 
{
    //public GameObject menuLoadScreen;
    public AudioSource menuSelectionSound;
    AudioSource myMenuSelectionSound;

    void Start()
    {
        myMenuSelectionSound = menuSelectionSound.GetComponent<AudioSource>();
    }

    public void play()
    {
        Instantiate(myMenuSelectionSound);
        //menuLoadScreen.SetActive(true);
        Time.timeScale = 1;
        Invoke("LoadMainMenu", 1.5f);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class quitScript : MonoBehaviour 
{
    public GameObject menuLoadScreen;
    public AudioSource menuSelectionSound;
    AudioSource myMenuSelectionSound;

    void Start()
    {
        myMenuSelectionSound = menuSelectionSound.GetComponent<AudioSource>();
    }

    public void play()
    {
        Instantiate(myMenuSelectionSound);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Invoke("LoadMainMenu", 2f);
        LoadMainMenu();
    }

    void LoadMainMenu()
    {
        //SceneManager.LoadScene("MainMenu");
        menuLoadScreen.SetActive(true);
    }
}

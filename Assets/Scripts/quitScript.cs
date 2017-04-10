using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class quitScript : MonoBehaviour 
{
    //public GameObject menuLoadScreen;
   // public AudioSource puzzleStart;
   // AudioSource myPuzzleStart;

    void Start()
    {
        //myPuzzleStart = puzzleStart.GetComponent<AudioSource>();
    }

    public void play()
    {
        //Instantiate(myPuzzleStart);
        //menuLoadScreen.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() //Loads up the second Scene of index 1 (Enters the bowling game)
    {
        SceneManager.LoadSceneAsync(1);
    }
    
    public void QuitGame() //Quits the application (leaving the game)
    {
        Application.Quit();
        Debug.Log("Left Game"); //This is for testing purposes, insures line 15 works
    }
}

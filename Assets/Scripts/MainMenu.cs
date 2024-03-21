using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private static int difficulty = 0;
    private static int extraLanes = 0;
    private static int[] difficultyScores = { 90, 125, 175, 220, 269 };

    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    public void PlayGame() //Loads up the second Scene of index 1 (Enters the bowling game)
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ReturnToMainMenu() //Loads up the first Scene of index 0 (Returns to main menu)
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame() //Quits the application (leaving the game)
    {
        Application.Quit();
    }

    public void ChangeLanes(TMP_Dropdown lane) { extraLanes = lane.value; }
    // Returns the number of lanes used, +1 as we already have a lane by default
    public int GetLanes(){ return extraLanes +1;}

    // Changes difficulty to whatever is picked, called whenever the value is changed
    public void ChangeDifficulty(TMP_Dropdown diff)
    {
        difficulty = diff.value;
    }
    // Returns the corresponding score based on difficulty
    public int GetDifficultyScore()
    {
        return difficultyScores[difficulty];
    }
}

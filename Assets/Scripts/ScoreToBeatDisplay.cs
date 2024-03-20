using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreToBeatDisplay : MonoBehaviour
{
    [SerializeField] private MainMenu sceneController;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = "Score to beat : " + sceneController.GetDifficultyScore().ToString();
    }
}

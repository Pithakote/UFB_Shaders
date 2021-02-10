using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int score;
    string timeLeft;
    int numTimeLeft;
    int baseScore;  // the amount of score you get for completing a level, without any bonuses.
    [SerializeField] TMPro.TMP_Text scoreText;
    void Start()
    {
        score = 0;
        baseScore = 1000;
    }

    private void Update()
    {
       
        if (CheckLevelWin.isWin)
        {
            timeLeft = Timer.minutes + Timer.seconds;
            numTimeLeft = int.Parse(timeLeft);
            
        }

        //Debug.Log("Time left = " + numTimeLeft);
    }

    public void showScore()
    {
        score = score + baseScore + (numTimeLeft * 28);
        scoreText.text = score.ToString();
    }

}

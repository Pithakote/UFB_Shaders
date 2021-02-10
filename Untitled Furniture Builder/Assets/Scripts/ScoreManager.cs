﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : Timer
{
    
    float timeLeft;
    int numTimeLeft;
    int baseScore;  // the amount of score you get for completing a level, without any bonuses.
    int score;
    int levelRating;

    [SerializeField]
    TMPro.TMP_Text scoreText;
    
    [SerializeField]
    Image oneStar;
    [SerializeField]
    Image twoStar;
    [SerializeField]
    Image threeStar;

    

    void Start()
    {
        score = 0;
        baseScore = 1000;
    }

    private void Update()
    {
       
        if (CheckLevelWin.isWin)
        {
            timeLeft = ((int)Timer.time / 60) + (Timer.time % 60);

            numTimeLeft = (int)timeLeft;

           
            //if (score >= 3800)
            //    levelRating = 3;
            //else if (score <= 3799 && score >= 3200)
            //    levelRating = 2;
            //else if (score <= 3199)
            //    levelRating = 1;

            //if (numTimeLeft >= startTime * 0.75f)
            //    levelRating = 3;
            //else if (numTimeLeft <= startTime * 0.74f && numTimeLeft >= startTime * 0.50f)
            //    levelRating = 2;
            //else if (numTimeLeft <= startTime * 0.49f)
            //    levelRating = 1;
            //else if (numTimeLeft >= 0)
            //    return;

            Debug.Log(score);
            Debug.Log("lvlRating" + levelRating);
        }

        

        if (levelRating == 1)
        {
            oneStar.color = new Color(255, 219, 1, 255)/255;         
        }
        else if (levelRating == 2)
        {
            oneStar.color = new Color(255, 219, 1, 255) / 255;
            twoStar.color = new Color(255, 219, 1, 255) / 255;            
        }
        else if (levelRating == 3)
        {
            oneStar.color = new Color(255, 219, 1, 255) / 255;
            twoStar.color = new Color(255, 219, 1, 255) / 255;
            threeStar.color = new Color(255, 219, 1, 255) / 255;
        }

        //Debug.Log("Time left = " + numTimeLeft);
    }

    public void showScore()
    {
        score = score + baseScore + (numTimeLeft * 56);
        scoreText.text = score.ToString();
        
    }

   

}

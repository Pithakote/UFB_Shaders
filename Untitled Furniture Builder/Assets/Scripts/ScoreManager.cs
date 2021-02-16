using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
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

    public SaveObject so;

    void Start()
    {
        score = 0;
        baseScore = 1000;
        Debug.Log("startTime = " + Timer.initialTime);
    }

    private void Update()
    {
       
        if (CheckLevelWin.isWin)
        {
            //timeLeft = ((int)Timer.time / 60) + (Timer.time % 60);
            timeLeft = int.Parse(Timer.minutes)*60 + int.Parse(Timer.seconds);

            numTimeLeft = (int)timeLeft;


            //if (score >= 3800)
            //    levelRating = 3;
            //else if (score <= 3799 && score >= 3200)
            //    levelRating = 2;
            //else if (score <= 3199)
            //    levelRating = 1;

            if (numTimeLeft >= Timer.initialTime * 0.75f)
                levelRating = 3;
            else if (numTimeLeft <= Timer.initialTime * 0.749999f && numTimeLeft >= Timer.initialTime * 0.50f)
                levelRating = 2;
            else if (numTimeLeft <= Timer.initialTime * 0.49f)
                levelRating = 1;
            else if (numTimeLeft <= 0)
                levelRating = 0;

            Debug.Log(score);
            Debug.Log("lvlRating" + levelRating);
            Debug.Log("numTimeLeft = " + numTimeLeft);
            
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


        if (so.tutorialComplete)
            so.levelProgress = 1;
            so.canSpawnRadio = true;
        if (so.l1Complete)
            so.levelProgress = 2;
        if (so.l2Complete)
            so.levelProgress = 3;
        if (so.l3Complete)
            so.levelProgress = 4;
        if (so.l4Complete)
            so.levelProgress = 5;

        Debug.Log("Radio: " + so.canSpawnRadio);
            



        SaveManager.Save(so);
        //Debug.Log("Time left = " + numTimeLeft);
    }

    public void showScore()
    {
        score = score + baseScore + (numTimeLeft * 56);
        scoreText.text = score.ToString();
        SaveManager.Save(so);
    }

    public void Reset()
    {
        CheckLevelWin.isWin = false;
    }

    public void levelComplete()
    {
        
        if (SceneManager.GetActiveScene().name == "Level_Radio")
        {
            so.tutorialComplete = true;
            so.tutorialRating = levelRating;
            SaveManager.Save(so);
        }
        else if (SceneManager.GetActiveScene().name == "Level_1")
        {
            so.l1Complete = true;
            so.l1Rating = levelRating;
            SaveManager.Save(so);
        }
        else if (SceneManager.GetActiveScene().name == "Level_2")
        {
            so.l2Complete = true;
            so.l2Rating = levelRating;
            SaveManager.Save(so);
        }
        else if (SceneManager.GetActiveScene().name == "Level_3")
        {
            so.l3Complete = true;
            so.l3Rating = levelRating;
            SaveManager.Save(so);
        }
        

        //so.levelProgress++;


    }

    private void OnLevelWasLoaded(int level)
    {
        so = SaveManager.Load();
    }

}

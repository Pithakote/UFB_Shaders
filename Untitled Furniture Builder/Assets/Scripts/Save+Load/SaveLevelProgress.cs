using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveLevelProgress : MonoBehaviour
{    

    public int levelProgressPref;
    public TMPro.TMP_Text progressText;

    

    public void completeLevel()
    {
        levelProgressPref++;
        PlayerPrefs.SetInt("levelProgress", levelProgressPref);    
       
        PlayerPrefs.Save();
        Debug.Log("Your level progress is : " + PlayerPrefs.GetInt("levelProgress"));
    }

    public void resetProgress()
    {
        levelProgressPref = 1;
        PlayerPrefs.SetInt("levelProgress", levelProgressPref);
    }

    

    private void Update()
    {
        levelProgressPref = PlayerPrefs.GetInt("levelProgress");
        progressText.text = "Level progress = " + levelProgressPref;
       
    }


}


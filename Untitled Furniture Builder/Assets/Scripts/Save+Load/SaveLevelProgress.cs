using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveLevelProgress : MonoBehaviour
{    

    public int levelProgress;
    public TMPro.TMP_Text progressText;

    

    public void completeLevel()
    {
        levelProgress++;
        PlayerPrefs.SetInt("levelProgress", levelProgress);    
       
        PlayerPrefs.Save();
        Debug.Log("Your level progress is : " + PlayerPrefs.GetInt("levelProgress"));
    }

    public void resetProgress()
    {
        levelProgress = 1;
        PlayerPrefs.SetInt("levelProgress", levelProgress);
    }

    

    private void Update()
    {
        levelProgress = PlayerPrefs.GetInt("levelProgress");
        progressText.text = "Level progress = " + levelProgress;
       
    }


}


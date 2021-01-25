using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLevelProgress : MonoBehaviour
{
    public Text levelProgText;
    public int levelProgress;


    public void completeLvl1()
    {
        levelProgress++;
        
    }
    public void completeLvl2()
    {
        levelProgress++;
        
    }
    public void completeLvl3()
    {
        levelProgress++;

    }

    public void saveProgress()
    {
        PlayerPrefs.SetInt("levelProgress", levelProgress);
        PlayerPrefs.Save();
        levelProgText.text = "Level progress = " + levelProgress;
        Debug.Log("Your level progress is : " + PlayerPrefs.GetInt("levelProgress"));
    }

    public void loadProgress()
    {
        levelProgress = PlayerPrefs.GetInt("levelProgress");
        levelProgText.text = "Level progress = " + levelProgress;
        Debug.Log("Your level progress is : " + PlayerPrefs.GetInt("levelProgress"));
    }
}
    

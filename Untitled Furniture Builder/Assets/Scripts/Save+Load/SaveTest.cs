using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveTest : MonoBehaviour
{
    public SaveObject so;
    //public TMPro.TMP_Text levelProgText;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Save
            SaveManager.Save(so);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Load
            so = SaveManager.Load();
        }



        //levelProgText.text = "Level Progress: " + so.levelProgress;
    }

    

    public void CompleteLevel()
    {
        so.levelProgress++;
    }

    public void ResetProgress()
    {

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        so.levelProgress = 0;
        so.tutorialRating = 0;
        so.l1Rating = 0;
        so.l2Rating = 0;
        so.l3Rating = 0;
        so.l4Rating = 0;
        so.l5Rating = 0;
        
        SaveManager.Save(so);

    }
}

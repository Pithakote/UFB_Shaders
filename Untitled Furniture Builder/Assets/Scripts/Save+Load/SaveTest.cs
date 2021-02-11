using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveTest : MonoBehaviour
{
    public SaveObject so;
    public TMPro.TMP_Text levelProgText;
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

        levelProgText.text = "Level Progress: " + so.levelProgress;
    }

    public void CompleteLevel()
    {
        so.levelProgress++;
    }
}

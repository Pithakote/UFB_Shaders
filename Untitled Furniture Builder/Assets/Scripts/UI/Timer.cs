using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;    
    public float startTime;
    public static float time;
    //public GameObject TimerUI;
    public GameObject GameOverUI;
    public static string minutes;
    public static string seconds;

   
    // Start is called before the first frame update
    void Start()
    {
        //startTime = 60.0f;
        time = startTime;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!CheckLevelWin.isWin)
        {
            time -= Time.deltaTime;
            minutes = ((int)time / 60).ToString();
            seconds = (time % 60).ToString("00");

            timeText.text = minutes + ":" + seconds;
            Debug.Log(time);
        }
        else
            return;
       

        //need to load a different scene or UI text when timer hits 0
        // for now it just resets
        if (startTime <= 0)
        {
            startTime = 0;
            gameObject.SetActive(false);
            GameOverUI.SetActive(true);
        }
    }
}

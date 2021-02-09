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
    public GameObject TimerUI;
    public GameObject GameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        //startTime = 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        startTime -= Time.deltaTime;
        string minutes = ((int)startTime / 60).ToString();
        string seconds = (startTime % 60).ToString("00");

        timeText.text = minutes + ":" + seconds;

        //need to load a different scene or UI text when timer hits 0
        // for now it just resets
        if (startTime == 0)
        {
            startTime = 0;
            TimerUI.SetActive(false);
            GameOverUI.SetActive(true);
        }
    }
}

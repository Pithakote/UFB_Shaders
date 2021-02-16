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
    public static float initialTime;
    //public GameObject TimerUI;
    public GameObject GameOverUI;
    public static string minutes;
    public static string seconds;

    List<GameObject> _children;
    bool _isOver;
    // Start is called before the first frame update
    void Awake()
    {
        //startTime = 60.0f;
        time = startTime;
        initialTime = time;
        _children = new List<GameObject>();
        _isOver = false;
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
            //Debug.Log(time);
        }
        else
            return;
       

        //need to load a different scene or UI text when timer hits 0
        // for now it just resets
        if (time <= 0 && _isOver == false)
        {
            
            //gameObject.SetActive(false);
            GameOverUI.SetActive(true);

            for (int i = 0; i < GameOverUI.transform.childCount; i++)
            {
                //if the InnerButtonAddListener is not present continue the loop but don't add
                if (GameOverUI.transform.GetChild(i).GetComponentInChildren<InnerButtonAddListener>() == null)
                    continue;

                //  if (_nextUI.gameObject.transform.GetChild(i).GetComponentInChildren<InnerButtonAddListener>() != null)
                _children.Add(GameOverUI.transform.GetChild(i).gameObject);
            }
            _children.ForEach(childr => childr.GetComponent<InnerButtonAddListener>().MoveToScreen());

            _isOver = true;
        }
    }
}

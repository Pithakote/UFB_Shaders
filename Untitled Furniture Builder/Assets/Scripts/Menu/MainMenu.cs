﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject oneStars;
  

    public GameObject[] levels;

   
    void Start()
    {
        
       if(SceneManager.GetActiveScene().name == "MainMenu")
       {
           Instantiate(oneStars, levels[0].transform);
       }
       
    }
    /*
    public void LoadLevel()
    {
        SceneManager.LoadScene("InstructionsTesting");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    */
    

    
}

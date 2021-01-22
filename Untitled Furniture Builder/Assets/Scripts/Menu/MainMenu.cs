using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject oneStars;
  

    public GameObject[] levels;

   
    void Start()
    {
       
        Instantiate(oneStars, levels[0].transform);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("InstructionsTesting");
    }

    

    
}

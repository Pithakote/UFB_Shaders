using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject oneStars;
  

    public GameObject[] levels;

   
    void Start()
    {
       
        Instantiate(oneStars, levels[0].transform);
    }

    

    
}

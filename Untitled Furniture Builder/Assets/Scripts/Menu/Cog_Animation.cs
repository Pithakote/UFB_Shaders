using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cog_Animation : MonoBehaviour
{
   
    void Update()
    {
        if(gameObject.name == "cog1")
        {
            transform.Rotate(0, 0, 60 * Time.deltaTime); // rotates 60 degrees per second around the z axis
        }
        else if(gameObject.name == "cog2")
        {
            transform.Rotate(0, 0, -60 * Time.deltaTime); // rotates -60 degrees per second around the z axis
        }

        
    }
}

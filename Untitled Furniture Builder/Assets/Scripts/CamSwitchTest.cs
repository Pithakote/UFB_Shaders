using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamSwitchTest : MonoBehaviour
{
    [SerializeField]
    private GameObject instructionsUI;
    [SerializeField]
    private GameObject instructionsButton;

  

   
    private void Update()
    {
        // checking to see if the instructions on the desk have been clicked
        if (Input.GetMouseButtonDown(0))
        {
            //pressed
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject;

                if (obj != null && obj.tag == "Instructions")
                {
                    print("hit");
                    instructionsUI.SetActive(true);
                    instructionsButton.SetActive(true);
                }
            }
        }

        
    }
}

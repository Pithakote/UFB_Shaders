using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{

    [SerializeField] GameObject checkButton;
    private void OnTriggerEnter(Collider other)
    {
        checkButton.SetActive(true);
        
    }

    private void OnTriggerExit(Collider other)
    {
        checkButton.SetActive(false);
    }


}

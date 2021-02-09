using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{

    [SerializeField] GameObject checkButton;

    CheckTrigger _enteredObject;
    public CheckTrigger EnteredObject { get { return _enteredObject; } }

    private void OnTriggerEnter(Collider other)
    {
        _enteredObject = other.gameObject.GetComponent<CheckTrigger>();
        checkButton.SetActive(true);      
        
    }

    private void OnTriggerExit(Collider other)
    {
        checkButton.SetActive(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{

    [SerializeField] GameObject checkButton;

    public CheckTrigger _enteredObjectLocal;
    public CheckTrigger EnteredObject { get { return _enteredObjectLocal; } }

    private void OnTriggerEnter(Collider other)
    {
        //it's sometimes getting trigger cubes not the cylinder
        //so that's why check trigger class isn't being detected

        if (other.gameObject.GetComponent<CheckTrigger>())//if the culinder touches the trigger
        {
            Debug.Log(other.gameObject.name);
            _enteredObjectLocal = other.gameObject.GetComponent<CheckTrigger>();
        }
        /*
        else if (other.gameObject.transform.parent.GetComponent<TriggerCheck>())//checks for trigger points
        {
            Debug.Log(other.gameObject.name);
            _enteredObjectLocal = other.gameObject.transform.parent.GetComponent<CheckTrigger>();
        }
        else if (other.gameObject.transform.parent.GetComponent<ScrewSnapToPos>())//screw check
        {
            Debug.Log(other.gameObject.name);
            _enteredObjectLocal = other.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<CheckTrigger>();
        }
        else if (other.gameObject.transform.parent.GetComponent<snapToPos>())//check for legs                 
        {
            Debug.Log(other.gameObject.name);
            _enteredObjectLocal = other.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<CheckTrigger>();//parent class
        }
        */
        checkButton.SetActive(true);
        //checkbutton will work regardless of the conditions

        //if the child touches the trigger it's parent is guranteed to have a class

        //optional if the "triggercheck" class is being checked in the else if
        //NOTE: if legs or screws are attached, the trigger might pick up on those first,
        //then the else if won't work. the trigger should ignore them and this
        //can be achieved by using the layers in the physics settings of unity.
    }

    private void OnTriggerExit(Collider other)
    {
        checkButton.SetActive(false);
    }


}

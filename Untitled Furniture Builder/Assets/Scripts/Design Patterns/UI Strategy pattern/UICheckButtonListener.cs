using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICheckButtonListener : UIButtonListener
{

    [SerializeField] private GameObject _enterTriggerPoint;// = new CheckWin();
   
   
    protected override void ButtonAction()
    {
        _instance.ButtonListenerManager.PerformButtonBehaviour(ReturnButtonBehaviour());
        // _instance.ButtonListenerManager.PerformButtonBehaviour(ReturnButtonBehaviour());
        // if (_instance == null)
        //     Debug.Log("Singleton instance is null");
        //
        // else
        // {
        //     if (_enterTriggerPoint.GetComponent<CheckWin>().EnteredObject.checkChildren())
        //         _instance.ButtonListenerManager.PerformButtonBehaviour(ReturnButtonBehaviour());
        //     else
        //         Debug.Log("UI Check button listener not triggered");
        // }
    }
}

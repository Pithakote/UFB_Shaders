using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICheckButtonListener : UIButtonListener
{

    [SerializeField] CheckWin _enterTriggerPoint;
    protected override void ButtonAction()
    {
        if (_enterTriggerPoint.EnteredObject.checkChildren())
            base.ButtonAction();
        else
            Debug.Log("UI Check button listener not triggered");



    }
}

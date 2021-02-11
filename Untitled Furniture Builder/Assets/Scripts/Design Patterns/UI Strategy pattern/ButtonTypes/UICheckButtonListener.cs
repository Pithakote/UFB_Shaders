using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICheckButtonListener : UIButtonListener
{

    [SerializeField] private GameObject _enterTriggerPoint;// = new CheckWin();
   
   
    protected override void ButtonAction()
    {
        _instance.ButtonListenerManager.PerformButtonBehaviour(ReturnButtonBehaviour());
     
    }
}

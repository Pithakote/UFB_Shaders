using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMainMenuManager : LocalManager
{
   
    protected override void SetInitialState()
    {

        _instance.StateManager.CurrentState = _instance.StateManager.MainMenuState;

      
    }

    

   




}
